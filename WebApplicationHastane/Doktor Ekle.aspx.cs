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
    public partial class Doktor_Ekle : System.Web.UI.Page
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
            if(Label1.Text==null)
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

            //var collection = database.GetCollection<doktor>("doktorlistesi");
            //var serviceList = collection.Find(x => x._id == ObjectId.Parse("5d4153d67eadf53370f3201e") && x.ServisList.Count > 0).FirstOrDefault()?.ServisList ?? new List<servis>();
            //var filt = serviceList.FindAll(x => x._id != ObjectId.Parse("5d4153e57eadf53370f3201f"));
            //ddlServis.DataSource = filt;
            //ddlServis.DataBind();
        }
        protected void ddlDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<doktortek>("doktorlistesi");
            var builder = Builders<doktortek>.Filter;
            var filter = builder.Eq("_id", ObjectId.Parse(ddlDoktor.SelectedValue));
            var result = collection.Find(filter).ToList();
            foreach (var item in result)
            {
                adıText.Value = item.doktor_adi.ToString();
                soyadText.Value = item.doktor_soyadi.ToString();
                bölümText.Value = item.doktor_bölüm.ToString();
            }
        }

        protected void kaydetbuton_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<doktortek>("doktorlistesi");
            doktortek cat = new doktortek();
            cat.doktor_adi = adıText.Value;
            cat.doktor_soyadi = soyadText.Value;
            cat.doktor_bölüm = bölümText.Value;
            collection.InsertOne(cat);
        }

        protected void güncellebuton_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<doktortek>("doktorlistesi");
            var filt = Builders<doktortek>.Filter.Eq("_id", ObjectId.Parse(ddlDoktor.SelectedValue));
            var update = Builders<doktortek>.Update.Set("doktor_adi", adıText.Value).Set("doktor_soyadi", adıText.Value).Set("doktor_bölüm", bölümText.Value);
            collection.UpdateOne(filt, update);
            var drcol = database.GetCollection<yatanhastalar>("yatanhastalar");
            var drfilt = Builders<yatanhastalar>.Filter.Eq("_id", ObjectId.Parse(ddlDoktor.SelectedValue));
            var drupdate = Builders<yatanhastalar>.Update.Set("doktor_adi", adıText.Value).Set("doktor_soyadi", adıText.Value).Set("doktor_bölüm", bölümText.Value);
            drcol.UpdateOne(drfilt, drupdate);
        }

        protected void silbuton_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<doktortek>("doktorlistesi");
            var filt = Builders<doktortek>.Filter.Eq("_id", ObjectId.Parse(ddlDoktor.SelectedValue));
            collection.DeleteOne(filt);
            var drcol = database.GetCollection<yatanhastalar>("yatanhastalar");
            var drfilt = Builders<yatanhastalar>.Filter.Eq("_id", ObjectId.Parse(ddlDoktor.SelectedValue));
            drcol.DeleteOne(drfilt);
        }

        //protected void servisekleButton_Click(object sender, EventArgs e)
        //{
        //    MongoClient client = new MongoClient();
        //    var database = client.GetDatabase("hastane");
        //    var collection = database.GetCollection<doktor>("doktorlistesi");
        //    servis cat = new servis();
        //    cat._id = ObjectId.Parse(ddlServis.SelectedValue);
        //    cat.servis_adi = ddlServis.SelectedItem.Text;
        //    //var doctor = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).FirstOrDefault().ServisList;
        //    List<servis> service = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).FirstOrDefault()?.ServisList ?? new List<servis>();
        //    service.Add(cat);
        //    collection.UpdateOne(Builders<doktor>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
        //                    Builders<doktor>.Update.Set(b => b.ServisList, service));
        //}
    }
}