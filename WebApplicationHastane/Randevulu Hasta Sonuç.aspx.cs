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
    public partial class Randevulu_Hasta_Sonuç : System.Web.UI.Page
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
            Label2.Text ="Tarih : "+ DateTime.UtcNow.ToShortDateString();
            Label2.Text = "27.09.2019";
            Label3.Text="Saat : "+ DateTime.UtcNow.ToShortTimeString();
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var doktorlistesi = database.GetCollection<doktortek>("doktorlistesi").AsQueryable<doktortek>().Select(k => new
            {
                AdSoyad = k.doktor_adi + " " + k.doktor_soyadi,
                Id = k._id
            }).ToList();
            ddlDoktor.DataSource = doktorlistesi;
            ddlDoktor.DataBind();
            epikrizText.Disabled = true;
            Button1.Enabled = false;
        }

        protected void ddlDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<randevu>("randevulistesi");
            var randevu = collection.Find(x => x.tarih == Label2.Text).ToList();
            var randevudoktor = randevu.SelectMany(x => x.DoktorList).Where(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList();
            var randevuhasta = randevudoktor.SelectMany(x => x.HastaList).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("TC");
            dt.Columns.Add("Hasta Adı");
            dt.Columns.Add("Hasta Soyadı");
            dt.Columns.Add("Telefon Numarası");
            dt.Columns.Add("Doğum Yılı");            
            dt.Columns.Add("Saat");
            foreach (var item in randevuhasta)
            {
                string[] yazi = {item.tc_no.ToString(), item.hasta_adi.ToString(), item.hasta_soyadi.ToString(), item.hasta_telno.ToString(), item.hasta_dogumyili.ToString(), item.saat.ToString() };
                dt.Rows.Add(yazi);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<randevu>("randevulistesi");
            var randevu = collection.Find(x => x.tarih == Label2.Text).ToList();
            var randevudoktor = randevu.SelectMany(x => x.DoktorList).Where(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList();
            var randevuhasta = randevudoktor.SelectMany(x => x.HastaList).Where(x=>x.tc_no==Convert.ToInt64(GridView1.SelectedRow.Cells[1].Text)).ToList();
            if (randevuhasta.FirstOrDefault().epikriz == null|| randevuhasta.FirstOrDefault().epikriz == "")
            {
                randevuhasta.FirstOrDefault().epikriz=epikrizText.Value;
                var filter = Builders<randevu>.Filter.Eq(x => x._id, randevu.FirstOrDefault()._id);
                var update = Builders<randevu>.Update.Set(b => b.DoktorList, randevu.FirstOrDefault().DoktorList);
                collection.UpdateOne(filter, update);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Hastaya epikriz sonucu başarı ile kaydedildi.');", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Önceden epikriz kaydı bulumaktadır. Epikriz : "+ randevuhasta.FirstOrDefault().epikriz + "');", true);

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Select") == 0)
            {
                var index = Convert.ToInt32(e.CommandArgument);
                MongoClient client = new MongoClient();
                var database = client.GetDatabase("hastane");
                var collection = database.GetCollection<randevu>("randevulistesi");
                var randevu = collection.Find(x => x.tarih == Label2.Text).ToList();
                var randevudoktor = randevu.SelectMany(x => x.DoktorList).Where(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList();
                var randevuhasta = randevudoktor.SelectMany(x => x.HastaList).Where(x => x.tc_no == Convert.ToInt64(GridView1.Rows[index].Cells[1].Text)).ToList();
                if (randevuhasta.FirstOrDefault().epikriz == null || randevuhasta.FirstOrDefault().epikriz == "")
                {
                    epikrizText.Disabled = false;
                    Button1.Enabled = true;
                }                    
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Önceden epikriz kaydı bulumaktadır. Epikriz : " + randevuhasta.FirstOrDefault().epikriz + "');", true);
            }
        }
    }
}