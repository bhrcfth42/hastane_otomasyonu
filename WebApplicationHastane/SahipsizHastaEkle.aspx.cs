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
    public partial class SahipsizHastaEkle : System.Web.UI.Page
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
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<hastatek>("hastalistesi");
            hastatek cat = new hastatek();
            cat.hasta_tc = Convert.ToInt64(tcText.Value);
            cat.hasta_adi = adiText.Value;
            cat.hasta_soyadi = SoyadiText.Value;
            cat.hasta_anneadi = anneText.Value;
            cat.hasta_babaadi = babaText.Value;
            cat.hasta_adres = adresText.Value;
            cat.hasta_telefon = Convert.ToInt64(telefonText.Value);
            cat.hasta_cinsiyet = cinsiyetRbl.SelectedValue;
            collection.InsertOne(cat);
        }
    }
}