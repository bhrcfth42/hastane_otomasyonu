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
    public partial class HastaBilgiGüncelle : System.Web.UI.Page
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
            var hastalistesi = database.GetCollection<hastatek>("hastalistesi").AsQueryable<hastatek>().Select(k => new
            {
                AdSoyad = k.hasta_adi + " " + k.hasta_soyadi,
                ID = k._id
            }).ToList();
            //var doktorlistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList);
            //var servislistesi = doktorlistesi.SelectMany(x => x.HastaList).Select(k => new { AdSoyad = k.hasta_adi + " " + k.hasta_soyadi, ID = k._id }).ToList(); ;
            ddlHasta.DataSource = hastalistesi;
            ddlHasta.DataBind();
        }

        protected void ddlHasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<hastatek>("hastalistesi");
            var hastalistesi = collection.Find(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            //var doktorlistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList);
            //var servislistesi = doktorlistesi.SelectMany(x => x.HastaList).ToList().Where(y => y._id == ObjectId.Parse(ddlHasta.SelectedValue));
            foreach (var hasta in hastalistesi)
            {
                tcText.Value = hasta.hasta_tc.ToString();
                adiText.Value = hasta.hasta_adi.ToString();
                SoyadiText.Value = hasta.hasta_soyadi.ToString();
                anneText.Value = hasta.hasta_anneadi.ToString();
                babaText.Value = hasta.hasta_babaadi.ToString();
                adresText.Value = hasta.hasta_adres.ToString();
                telefonText.Value = hasta.hasta_telefon.ToString();
                cinsiyetRbl.SelectedValue = hasta.hasta_cinsiyet.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<hastatek>("hastalistesi");
            var filt = Builders<hastatek>.Filter.Eq("_id", ObjectId.Parse(ddlHasta.SelectedValue));
            var update = Builders<hastatek>.Update.Set("hasta_tc", tcText.Value).Set("hasta_adi", adiText.Value).Set("hasta_soyadi", SoyadiText.Value).Set("hasta_anneadi", anneText.Value).Set("hasta_babaadi", babaText.Value).Set("hasta_adres", adresText.Value).Set("hasta_telefon", Convert.ToInt64(telefonText.Value)).Set("hasta_cinsiyet", cinsiyetRbl.SelectedValue);
            collection.UpdateOne(filt, update);            
            //var hastacol = database.GetCollection<yatanhastalar>("yatanhastalar");
            //var hst = hastacol.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            //var filthst = Builders<yatanhastalar>.Filter.ElemMatch(x => x.ServisList, Builders<servis>.Filter.ElemMatch(x => x.HastaList, Builders<hasta>.Filter.Eq("_id", ObjectId.Parse(ddlHasta.SelectedValue))));
            //var updatehst = Builders<yatanhastalar>.Update.Set("ServisList.$[].HastaList", hst);
            //hastacol.UpdateOne(filthst, updatehst);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<hastatek>("hastalistesi");
            var filt = Builders<hastatek>.Filter.Eq("_id", ObjectId.Parse(ddlHasta.SelectedValue));           
            collection.DeleteOne(filt);
            var hstcol = database.GetCollection<yatanhastalar>("yatanhastalar");
            var filthst = Builders<yatanhastalar>.Filter.ElemMatch(x => x.ServisList, Builders<servis>.Filter.ElemMatch(x => x.HastaList, Builders<hasta>.Filter.Eq("_id", ObjectId.Parse(ddlHasta.SelectedValue))));
            hstcol.DeleteOne(filthst);
        }
    }
}