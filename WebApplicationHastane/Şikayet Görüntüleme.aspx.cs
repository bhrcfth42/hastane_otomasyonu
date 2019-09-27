using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationHastane.Models;

namespace WebApplicationHastane
{
    public partial class Şikayet_Görüntüleme : System.Web.UI.Page
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
            var collection = database.GetCollection<sikayetler>("sikayetler");
            var sonuclanmamis = collection.Find(x => x.sonuc == null).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Adı ve Soyadı");
            dt.Columns.Add("Telefon Numarası");
            dt.Columns.Add("Mail Adresi");
            dt.Columns.Add("Konu");
            dt.Columns.Add("Tarih");
            foreach (var item in sonuclanmamis)
            {
                string[] yazi = { item._id.ToString(), item.ad_soyad.ToString(), item.telefon_no.ToString(), item.mail_adress.ToString(), item.konu.ToString(), item.tarih.ToString() };
                dt.Rows.Add(yazi);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            var sonuclu = collection.Find(x => x.sonuc != null).ToList();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Id");
            dt2.Columns.Add("Adı ve Soyadı");
            dt2.Columns.Add("Telefon Numarası");
            dt2.Columns.Add("Mail Adresi");
            dt2.Columns.Add("Konu");
            dt2.Columns.Add("Tarih");
            foreach (var item in sonuclu)
            {
                string[] yazi = { item._id.ToString(), item.ad_soyad.ToString(), item.telefon_no.ToString(), item.mail_adress.ToString(), item.konu.ToString(), item.tarih.ToString() };
                dt2.Rows.Add(yazi);
            }
            GridView2.DataSource = dt2;
            GridView2.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<sikayetler>("sikayetler");
            var sonuc = collection.Find(x => x._id == ObjectId.Parse(GridView1.SelectedRow.Cells[1].Text));
            var filter = Builders<sikayetler>.Filter.Eq(k => k._id, ObjectId.Parse(GridView1.SelectedRow.Cells[1].Text));
            var update = Builders<sikayetler>.Update.Set(x => x.sonuc, sonucText.Value);
            collection.UpdateOne(filter, update);
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Select") == 0)
            {
                var index = Convert.ToInt32(e.CommandArgument);
                MongoClient client = new MongoClient();
                var database = client.GetDatabase("hastane");
                var collection = database.GetCollection<sikayetler>("sikayetler");
                var sikayet = collection.Find(x => x._id == ObjectId.Parse(GridView2.Rows[index].Cells[1].Text)).FirstOrDefault();
                Label3.Text = sikayet.konu + " konulu olarak gönderilmiş mesaj : " + sikayet.mesaj;
                Label4.Text = "Mesaja yapılan dönüş : " + sikayet.sonuc;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Select") == 0)
            {
                var index = Convert.ToInt32(e.CommandArgument);
                MongoClient client = new MongoClient();
                var database = client.GetDatabase("hastane");
                var collection = database.GetCollection<sikayetler>("sikayetler");
                var sikayet = collection.Find(x => x._id == ObjectId.Parse(GridView1.Rows[index].Cells[1].Text)).FirstOrDefault();
                Label2.Text =sikayet.konu+ " konulu olarak gönderilmiş mesaj : " + sikayet.mesaj;
            }
        }
    }
}