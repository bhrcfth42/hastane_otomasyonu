using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplicationHastane.Models;

namespace WebApplicationHastane
{
    public partial class Eczane_İlaç_Onay : System.Web.UI.Page
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
                case "Eczacı":
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
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var doktorlist = collection.Find(x => x._id != null).ToList();
            var servislist = doktorlist.SelectMany(x => x.ServisList).ToList();
            var hastalist = servislist.SelectMany(x => x.HastaList).ToList().Where(x => x.hasta_tedavi_durum == "Eczaneden Onay Bekliyor").Select(k => new
            {
                AdSoyad = k.hasta_adi + " " + k.hasta_soyadi,
                Id = k._id
            }).ToList();
            ddlHasta.DataSource = hastalist;
            ddlHasta.DataBind();
        }

        protected void ddlHasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxTedavi.Items.Clear();
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var doktorlist = collection.Find(x => x._id != null).ToList();
            var servislist = doktorlist.SelectMany(x => x.ServisList).ToList();
            var hastalist = servislist.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue));
            var ilaclist = hastalist.SelectMany(x => x.IlacList).ToList();
            foreach (var item in ilaclist)
            {
                string ilac = "Barkod=> " + item.barkod + "     Adı=> " + item.ilac_adi + "    Adet=>" + item.ilac_uygulanacak_adet;
                ListBoxTedavi.Items.Add(ilac);
            }
        }

        protected void btnonayla_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var doktorlist = collection.Find(x => x._id != null).ToList();
            var servislist = doktorlist.SelectMany(x => x.ServisList).ToList();
            var hastalist = servislist.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue));
            foreach (var item in hastalist)
            {
                item.hasta_tedavi_durum = "Onaylandı";
            }
            var filt = Builders<yatanhastalar>.Filter.ElemMatch(x => x.ServisList, Builders<servis>.Filter.ElemMatch(x => x.HastaList, Builders<hasta>.Filter.Eq(x => x._id, ObjectId.Parse(ddlHasta.SelectedValue))));
            var update = Builders<yatanhastalar>.Update.Set(x => x.ServisList, servislist);
            collection.UpdateOne(filt, update);
        }
    }
}