using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationHastane.Models;

namespace WebApplicationHastane
{
    public partial class Hasta_Randevu_Paneli : System.Web.UI.Page
    {
        ObjectId doktor_id;
        ObjectId randevu_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tc"] == null || Session["ad"] == null || Session["soyad"] == null || Session["yıl"] == null || Session["telno"] == null)
                Response.Redirect("Hasta Randevu.aspx");
            else
            {
                tc_no.Text = Session["tc"].ToString();
                adi.Text = Session["ad"].ToString();
                soyadi.Text = Session["soyad"].ToString();
                dogumyili.Text = Session["yıl"].ToString();
                telno.Text = Session["telno"].ToString();
            }
            if (IsPostBack)
                return;
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var doktorlistesi = database.GetCollection<doktortek>("doktorlistesi").AsQueryable<doktortek>().Select(k => new
            {
                bölüm = k.doktor_bölüm,
            }).ToList();
            ddldoktorbölüm.DataSource = doktorlistesi;
            ddldoktorbölüm.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<randevu>("randevulistesi");
            var tarihlist = collection.Find(x => x.tarih == ddltarih.SelectedItem.Text).ToList();
            if (tarihlist.Count == 0)
            {
                randevu cat = new randevu();
                cat.tarih = ddltarih.SelectedItem.Text;
                collection.InsertOne(cat);
            }
            tarihlist = collection.Find(x => x.tarih == ddltarih.SelectedItem.Text).ToList();
            randevu_id = tarihlist.FirstOrDefault()._id;
            doktor_id = ObjectId.Parse(ddldoktorad.SelectedValue);
            var doktorlist = tarihlist.SelectMany(x => x.DoktorList).Where(x => x.doktor_bölüm == ddldoktorbölüm.SelectedItem.Text && x._id == doktor_id).ToList();
            if (doktorlist.Count == 0)
            {
                var doktorlistesi = database.GetCollection<doktortek>("doktorlistesi").Find(x => x._id == ObjectId.Parse(ddldoktorad.SelectedValue)).FirstOrDefault();
                randevudoktor cat = new randevudoktor();
                cat._id = doktorlistesi._id;
                cat.doktor_adi = doktorlistesi.doktor_adi;
                cat.doktor_soyadi = doktorlistesi.doktor_soyadi;
                cat.doktor_bölüm = doktorlistesi.doktor_bölüm;
                List<randevudoktor> doktorliste = collection.Find(x => x._id == randevu_id).FirstOrDefault()?.DoktorList ?? new List<randevudoktor>();
                doktorliste.Add(cat);
                collection.UpdateOne(Builders<randevu>.Filter.Eq(x => x._id, randevu_id),
                            Builders<randevu>.Update.Set(b => b.DoktorList, doktorliste));
            }
            tarihlist = collection.Find(x => x.tarih == ddltarih.SelectedItem.Text).ToList();
            doktorlist = tarihlist.SelectMany(x => x.DoktorList).Where(x=>x._id == doktor_id).ToList();
            var hastalist = doktorlist.SelectMany(x => x.HastaList).Where(x => x.tc_no == Convert.ToInt64(tc_no.Text)).ToList();
            if(hastalist.Count==0)
            {                
                randevuhasta cat = new randevuhasta();
                cat.tc_no = Convert.ToInt64(tc_no.Text);
                cat.hasta_adi = adi.Text;
                cat.hasta_soyadi = soyadi.Text;
                cat.hasta_dogumyili = Convert.ToInt32(dogumyili.Text);
                cat.hasta_telno = telno.Text;
                cat.saat = ddlsaat.SelectedItem.Text;
                cat.randevu_alınma_zamanı = DateTime.UtcNow.ToShortDateString()+" "+DateTime.UtcNow.ToShortTimeString();
                var drlist = collection.Find(x => x._id == randevu_id).FirstOrDefault()?.DoktorList ?? new List<randevudoktor>();
                List<randevuhasta> hastaliste = drlist.FirstOrDefault()?.HastaList ?? new List<randevuhasta>();
                hastaliste.Add(cat);
                collection.UpdateOne(Builders<randevu>.Filter.Eq(x => x._id, randevu_id),
                            Builders<randevu>.Update.Set(b => b.DoktorList, drlist));
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Randevunuz "+ddltarih.SelectedItem.Text+" tarihinde ve saat "+ddlsaat.SelectedItem.Text+". Lütfen randevu saatinizden 10 dakika önce polikliniğe geliniz.');", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('"+hastalist.FirstOrDefault().randevu_alınma_zamanı+" Tarihinde Randevu Alınmış. "+"Tarihi: "+ddltarih.SelectedItem.Text+" Saati: "+ hastalist.FirstOrDefault().saat+ " Kayıtlı Randevunuz Bulunmaktadır.');", true);
        }

        protected void ddldoktorbölüm_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var doktorlistesi = database.GetCollection<doktortek>("doktorlistesi").Find(x=>x.doktor_bölüm==ddldoktorbölüm.SelectedItem.Text).ToList().Select(k => new
            {
                AdSoyad=k.doktor_adi+" "+k.doktor_soyadi,
                Id=k._id
            }).ToList();
            ddldoktorad.DataSource = doktorlistesi;
            ddldoktorad.DataBind();
        }

        protected void ddldoktorad_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddltarih.Items.Clear();
            for(int i=1;i<=5;i++)
            {
                ddltarih.Items.Add(DateTime.UtcNow.AddDays(i).ToShortDateString());
            }
            doktor_id = ObjectId.Parse(ddldoktorad.SelectedValue);
        }

        protected void ddltarih_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlsaat.Items.Clear();
            DateTime saat;
            saat = Convert.ToDateTime("09:30:00");
            for (int i = 0; i < 14; i++)
            {                
                saat=saat.AddMinutes(30);
                ddlsaat.Items.Add(saat.ToShortTimeString());
            }            
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<randevu>("randevulistesi");
            doktor_id = ObjectId.Parse(ddldoktorad.SelectedValue);
            var tarihlist = collection.Find(x => x.tarih == ddltarih.SelectedItem.Text).ToList();
            var doktorlist = tarihlist.SelectMany(x => x.DoktorList).Where(x => x.doktor_bölüm == ddldoktorbölüm.SelectedItem.Text && x._id == doktor_id).ToList();
            var hastalist = doktorlist.SelectMany(x => x.HastaList).ToList();
            foreach(var item in hastalist)
            {
                ddlsaat.Items.FindByText(item.saat).Enabled=false;
            }
            ddlsaat.Items.FindByText("12:00").Enabled = false;
            ddlsaat.Items.FindByText("12:30").Enabled = false;
            ddlsaat.Items.FindByText("13:00").Enabled = false;
        }
    }
}