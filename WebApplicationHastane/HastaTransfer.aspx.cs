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
    public partial class HastaTransfer : System.Web.UI.Page
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
                case "Sekreter":
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
            var doktorlist = database.GetCollection<yatanhastalar>("yatanhastalar").AsQueryable<yatanhastalar>().Select(k => new
            {
                AdSoyad = k.doktor_adi + " " + k.doktor_soyadi,
                ID = k._id
            }).ToList();
            ddlDoktorhasta.DataSource = doktorlist;
            ddlDoktorhasta.DataBind();

            var doktorlist2 = database.GetCollection<doktortek>("doktorlistesi").AsQueryable<doktortek>().Select(k => new
            {
                AdSoyad = k.doktor_adi + " " + k.doktor_soyadi,
                ID = k._id
            }).ToList();
            ddlDoktor.DataSource = doktorlist2;
            ddlDoktor.DataBind();

            var servislist = database.GetCollection<servistek>("servislistesi").AsQueryable<servistek>().Select(k => new
            {
                Ad = k.servis_adi,
                ID = k._id
            }).ToList();
            ddlServis.DataSource = servislist;
            ddlServis.DataBind();
        }

        protected void btnyatisyap_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");            
            var doktorlist = database.GetCollection<doktortek>("doktorlistesi").Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList();
            var dr = collection.Find(x => x._id == ObjectId.Parse(ddlDoktorhasta.SelectedValue)).ToList();
            if (doktorlist.Count != dr.Count)
            {
                foreach (var doktor in doktorlist)
                {
                    yatanhastalar cat = new yatanhastalar();
                    cat._id = doktor._id;
                    cat.doktor_adi = doktor.doktor_adi;
                    cat.doktor_soyadi = doktor.doktor_soyadi;
                    cat.doktor_bölüm = doktor.doktor_bölüm;
                    collection.InsertOne(cat);
                }
            }
            var srv = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList().SelectMany(x => x.ServisList).Where(x => x._id == ObjectId.Parse(ddlServis.SelectedValue)).ToList();
            var servislist = database.GetCollection<servistek>("servislistesi").Find(x => x._id == ObjectId.Parse(ddlServis.SelectedValue)).ToList();
            if (srv.Count != servislist.Count)
            {
                foreach (var item in servislist)
                {
                    servis cat = new servis();
                    cat._id = item._id;
                    cat.servis_adi = item.servis_adi;
                    List<servis> servisliste = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).FirstOrDefault()?.ServisList ?? new List<servis>();
                    servisliste.Add(cat);
                    collection.UpdateOne(Builders<yatanhastalar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
                                Builders<yatanhastalar>.Update.Set(b => b.ServisList, servisliste));
                }
            }
            var hst = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).Where(x=>x._id== ObjectId.Parse(ddlServis.SelectedValue)).ToList().SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            var hastalistesi = database.GetCollection<hastatek>("hastalistesi").Find(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            if (hst.Count != hastalistesi.Count)
            {
                foreach (var item in hastalistesi)
                {
                    hasta cat = new hasta();
                    cat._id = item._id;
                    cat.hasta_adi = item.hasta_adi;
                    cat.hasta_soyadi = item.hasta_soyadi;
                    cat.hasta_anneadi = item.hasta_anneadi;
                    cat.hasta_babaadi = item.hasta_babaadi;
                    cat.hasta_telefon = item.hasta_telefon;
                    cat.hasta_adres = item.hasta_adres;
                    cat.hasta_cinsiyet = item.hasta_cinsiyet;
                    var servisliste = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).FirstOrDefault()?.ServisList ?? new List<servis>();
                    List<hasta> hastalist = servisliste.FirstOrDefault(x => x._id == ObjectId.Parse(ddlServis.SelectedValue))?.HastaList ?? new List<hasta>();
                    hastalist.Add(cat);
                    collection.UpdateOne(Builders<yatanhastalar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
                                Builders<yatanhastalar>.Update.Set(b => b.ServisList, servisliste));                                       
                }
                var doktorId = ObjectId.Parse(ddlDoktorhasta.SelectedValue);
                var hasta = collection.Find(x => x._id == doktorId).ToList().SelectMany(x => x.ServisList).SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).FirstOrDefault();
                var filt = Builders<yatanhastalar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktorhasta.SelectedValue));
                var update = Builders<yatanhastalar>.Update.Pull("ServisList.$[].HastaList", hasta);
                collection.UpdateOne(filt, update);
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var hastalist = database.GetCollection<yatanhastalar>("yatanhastalar").AsQueryable<yatanhastalar>().Where(x=>x._id==ObjectId.Parse(ddlDoktorhasta.SelectedValue)).SelectMany(x => x.ServisList).SelectMany(x => x.HastaList).Select(k => new
            {
                AdSoyad = k.hasta_adi + " " + k.hasta_soyadi,
                ID = k._id
            }).ToList();
            ddlHasta.DataSource = hastalist;
            ddlHasta.DataBind();
        }
    }
}