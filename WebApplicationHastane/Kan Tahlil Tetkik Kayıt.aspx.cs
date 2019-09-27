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
    public partial class Kan_Tahlil_Tetkik_Kayıt : System.Web.UI.Page
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
                case "Laborant":
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
            var laboratuvarlistesi = database.GetCollection<laboratuvar>("kanlistesi").AsQueryable<laboratuvar>().Select(k => new
            {
                Ad = k.Tetkik_alan_adi,
                Id = k._id
            }).ToList();
            ddlTetkik.DataSource = laboratuvarlistesi;
            ddlTetkik.DataBind();
        }

        protected void Kaydetbutton_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<laboratuvar>("kanlistesi");
            laboratuvar cat = new laboratuvar();
            cat.Tetkik_alan_adi = LaboratuvarText.Value;
            collection.InsertOne(cat);
        }

        protected void TetkikKaydet_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<laboratuvar>("kanlistesi");
            tahlil cat = new tahlil();
            cat._id = ObjectId.GenerateNewId();
            cat.tahlil_adi = TahlilText.Value;
            List<tahlil> tahlilliste = collection.Find(x => x._id == ObjectId.Parse(ddlTetkik.SelectedValue)).FirstOrDefault()?.TahlilList ?? new List<tahlil>();
            tahlilliste.Add(cat);
            collection.UpdateOne(Builders<laboratuvar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlTetkik.SelectedValue)),
                        Builders<laboratuvar>.Update.Set(b => b.TahlilList, tahlilliste));
        }
    }
}