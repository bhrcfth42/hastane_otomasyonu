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
    public partial class Radyoloji_Tetkik_Kayıt : System.Web.UI.Page
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
                case "Radyoloji":
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
            var radyolojilistesi = database.GetCollection<radyoloji>("radyolojilistesi").AsQueryable<radyoloji>().Select(k => new
            {
                Ad = k.radyoloji_alan_adi,
                Id = k._id
            }).ToList();
            ddlRadyoloji.DataSource = radyolojilistesi;
            ddlRadyoloji.DataBind();
        }

        protected void Kaydetbutton_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<radyoloji>("radyolojilistesi");
            radyoloji cat = new radyoloji();
            cat.radyoloji_alan_adi = RadyolojiText.Value;
            collection.InsertOne(cat);
        }

        protected void TetkikKaydet_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<radyoloji>("radyolojilistesi");
            tetkik cat = new tetkik();
            cat._id = ObjectId.GenerateNewId();
            cat.tetkik_adi = TetkikText.Value;
            List<tetkik> tetkikliste = collection.Find(x => x._id == ObjectId.Parse(ddlRadyoloji.SelectedValue)).FirstOrDefault()?.TetkikList ?? new List<tetkik>();
            tetkikliste.Add(cat);
            collection.UpdateOne(Builders<radyoloji>.Filter.Eq(x => x._id, ObjectId.Parse(ddlRadyoloji.SelectedValue)),
                        Builders<radyoloji>.Update.Set(b => b.TetkikList, tetkikliste));
        }
    }
}