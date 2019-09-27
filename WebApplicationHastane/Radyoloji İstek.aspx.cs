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
    public partial class Radyoloji_İstek : System.Web.UI.Page
    {
        string yetki;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count == 0 || Request.QueryString == null || Request.QueryString.Get("yetki") == null)
                Response.Redirect("Login.aspx");
            else
                yetki = Request.QueryString["yetki"].ToString();
            switch (yetki)
            {
                case "Admin":
                    break;
                case "Doktor":
                    break;
                default:
                    Response.Redirect("Login.aspx");
                    break;
            }
            if (Session["adi"] == null || Session["soyadi"] == null)
                Response.Redirect("Login.aspx");
            else
                Label1.Text = Session["adi"] + " " + Session["soyadi"];
            if (Label1.Text == null)
                Response.Redirect("Login.aspx");
            if (IsPostBack)
                return;

            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var doktorlistesi = database.GetCollection<doktortek>("doktorlistesi").AsQueryable<doktortek>().Select(k => new
            {
                AdSoyad = k.doktor_adi + " " + k.doktor_soyadi,
                Id = k._id
            }).ToList();
            ddlDoktor.DataSource = doktorlistesi;
            ddlDoktor.DataBind();

            var radyolojilistesi = database.GetCollection<radyoloji>("radyolojilistesi").AsQueryable<radyoloji>().Select(k => new
            {
                Ad = k.radyoloji_alan_adi,
                Id = k._id
            }).ToList();
            ddlRadyoloji.DataSource = radyolojilistesi;
            ddlRadyoloji.DataBind();
        }

        protected void kaydetbuton_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList().SelectMany(x => x.ServisList);
            var hastalist = servislist.SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            var radyolojilist = hastalist.SelectMany(x => x.RadyolojiList).ToList();
            radyolojitetkikler cat = new radyolojitetkikler();
            cat._id = ObjectId.GenerateNewId();
            cat.tarih = DateTime.UtcNow.ToShortDateString();
            List<radyolojitetkikler> radlist = hastalist.Find(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue))?.RadyolojiList ?? new List<radyolojitetkikler>();
            hastalist.FirstOrDefault(k => k._id == ObjectId.Parse(ddlHasta.SelectedValue)).RadyolojiList.Add(cat);
            collection.UpdateOne(Builders<yatanhastalar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
                            Builders<yatanhastalar>.Update.Set(b => b.ServisList, servislist));
            for (int i = 0; i < ListBoxİstekler.Items.Count; i++)
            {
                tetkikler cata = new tetkikler();
                cata.tahlil_adi = ListBoxİstekler.Items[i].Text;
                List<tetkikler> radalist = radyolojilist.Find(x => x._id == cat._id)?.TetkiklerList ?? new List<tetkikler>();
                radyolojilist.FirstOrDefault(k => k._id == cat._id).TetkiklerList.Add(cata);
            }
            collection.UpdateOne(Builders<yatanhastalar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
                            Builders<yatanhastalar>.Update.Set(b => b.ServisList, servislist));
            //radyolojilist = hastalist.SelectMany(x => x.RadyolojiList).Where(x => x._id == ObjectId.Parse(id)).ToList();
            //var tetkiklist = radyolojilist.SelectMany(x => x.TetkiklerList).Where(x => x.tahlil_adi == ddlTetkik.SelectedItem.Text).ToList();
            //var tetkiklistesi = radyolojilistesi.SelectMany(x => x.TetkikList).Where(x => x._id == ObjectId.Parse(ddlTetkik.SelectedValue)).ToList();
            //if (tetkiklist.Count != tetkiklistesi.Count)
            //{


            //    collection.UpdateOne(Builders<yatanhastalar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
            //                    Builders<yatanhastalar>.Update.Set(b => b.ServisList, servislist));

            //    //var filter=Builders<yatanhastalar>.Filter.And(Builders<yatanhastalar>.Filter.Eq(x=>x._id, ObjectId.Parse(ddlDoktor.SelectedValue)), Builders<yatanhastalar>.Filter.ElemMatch(x=>x.ServisList, Builders<servis>.Filter.ElemMatch(x=>x.HastaList,Builders<hasta>.Filter.Eq(x=>x._id,ObjectId.Parse(ddlHasta.SelectedValue)))));
            //    //var hst = collection.Find(filter).SingleOrDefault();
            //    //var update = Builders<yatanhastalar>.Update;
            //    //var set = update.Set("ServisList.$[].HastaList.$.hasta_radyoloji_durum", "Radyoloji İsteği Bulunmaktadır.");
            //    //collection.FindOneAndUpdate(filter, set);
            //}
        }

        protected void ddlDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var hastalist = database.GetCollection<yatanhastalar>("yatanhastalar").AsQueryable<yatanhastalar>().Where(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).SelectMany(x => x.ServisList).SelectMany(x => x.HastaList).Select(k => new
            {
                AdSoyad = k.hasta_adi + " " + k.hasta_soyadi,
                ID = k._id
            }).ToList();
            ddlHasta.DataSource = hastalist;
            ddlHasta.DataBind();
            ddlHasta.SelectedIndex = -1;
        }

        protected void ddlRadyoloji_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var tetkiklist = database.GetCollection<radyoloji>("radyolojilistesi").AsQueryable<radyoloji>().Where(x => x._id == ObjectId.Parse(ddlRadyoloji.SelectedValue)).SelectMany(x => x.TetkikList).Select(k => new
            {
                Ad = k.tetkik_adi,
                ID = k._id
            }).ToList();
            ddlTetkik.DataSource = tetkiklist;
            ddlTetkik.DataBind();
            ddlTetkik.SelectedIndex = -1;
        }

        protected void ddlTetkik_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxİstekler.Items.Add(ddlRadyoloji.SelectedItem + " " + ddlTetkik.SelectedItem);
            ListBoxİstekler.SelectedIndex = -1;
        }
    }
}