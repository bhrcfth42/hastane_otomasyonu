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
    public partial class Kan_Tahlil_İstek : System.Web.UI.Page
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
            ddlDoktor.SelectedIndex = -1;

            var laboratuvarlistesi = database.GetCollection<laboratuvar>("kanlistesi").AsQueryable<laboratuvar>().Select(k => new
            {
                Ad = k.Tetkik_alan_adi,
                Id = k._id
            }).ToList();
            ddlKan.DataSource = laboratuvarlistesi;
            ddlKan.DataBind();
            ddlKan.SelectedIndex = -1;
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
        }

        protected void ddlKan_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var tahlillist = database.GetCollection<laboratuvar>("kanlistesi").AsQueryable<laboratuvar>().Where(x => x._id == ObjectId.Parse(ddlKan.SelectedValue)).SelectMany(x => x.TahlilList).Select(k => new
            {
                Ad = k.tahlil_adi,
                ID = k._id
            }).ToList();
            ddlTetkik.DataSource = tahlillist;
            ddlTetkik.DataBind();
        }

        protected void kaydetbuton_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList().SelectMany(x => x.ServisList);
            var hastalist = servislist.SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            var hastakanlist = hastalist.SelectMany(x => x.KanTahlilList).Where(x => x._id == ObjectId.Parse(ddlKan.SelectedValue)).ToList();
            var kanlistesi = database.GetCollection<laboratuvar>("kanlistesi").Find(x => x._id == ObjectId.Parse(ddlKan.SelectedValue)).ToList();
            kantahliltetkikler cat = new kantahliltetkikler();
            cat._id = ObjectId.GenerateNewId();
            cat.Tarih = DateTime.UtcNow.ToShortDateString();
            List<kantahliltetkikler> kanlist = hastalist.Find(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue))?.KanTahlilList ?? new List<kantahliltetkikler>();
            hastalist.FirstOrDefault(k => k._id == ObjectId.Parse(ddlHasta.SelectedValue)).KanTahlilList.Add(cat);
            collection.UpdateOne(Builders<yatanhastalar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
                            Builders<yatanhastalar>.Update.Set(b => b.ServisList, servislist));
            hastakanlist = hastalist.SelectMany(x => x.KanTahlilList).Where(x => x._id == cat._id).ToList();
            for (int i = 0; i < ListBoxİstekler.Items.Count; i++)
            {
                kantahlil cata = new kantahlil();
                cata.tahlil_adi = ListBoxİstekler.Items[i].Text;
                List<kantahlil> tahlillist = hastakanlist.Find(x => x._id == cat._id)?.TahlilList ?? new List<kantahlil>();
                hastakanlist.FirstOrDefault(k => k._id == cat._id).TahlilList.Add(cata);

            }
            collection.UpdateOne(Builders<yatanhastalar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
                            Builders<yatanhastalar>.Update.Set(b => b.ServisList, servislist));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ListBoxİstekler.Items.Add(ddlKan.SelectedItem+" "+ddlTetkik.SelectedItem);
            ListBoxİstekler.SelectedIndex = -1;
        }
    }
}