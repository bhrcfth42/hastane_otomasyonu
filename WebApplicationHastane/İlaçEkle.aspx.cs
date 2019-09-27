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
    public partial class İlaçEkle : System.Web.UI.Page
    {
        //System.Data.DataTable dt = new System.Data.DataTable("DataTable");
        //System.Data.DataSet ds = new System.Data.DataSet("DataSet");
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
                

            //MongoClient client = new MongoClient();
            //var database = client.GetDatabase("hastane");
            //var doktorlistesi = database.GetCollection<doktor>("doktorlistesi").AsQueryable<doktor>().Select(k => new
            //{
            //    AdSoyad = k.doktor_adi + " " + k.doktor_soyadi,
            //    IdD = k._id
            //}).ToList();
            //ddlDoktor.DataSource = doktorlistesi;
            //ddlDoktor.DataBind();

        }
        protected void btnilackaydet_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<ilactek>("ilaclistesi");
            ilactek cat = new ilactek();
            cat._id = ObjectId.GenerateNewId();
            cat.ilac_adi = adıText.Value;
            cat.barkod = barkodText.Value;
            cat.reçete_türü = recetetürüText.Value;
            cat.ilac_toplam_adet = Convert.ToInt32(adetText.Value);
            collection.InsertOne(cat);
            //var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).FirstOrDefault()?.ServisList ?? new List<servis>();
            //var hastalist = servislist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlServis.SelectedValue))?.HastaList ?? new List<hasta>();
            //List<ilac> ilaclist = hastalist.Find(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue))?.IlacList ?? new List<ilac>();
            //hastalist.FirstOrDefault(k => k._id == ObjectId.Parse(ddlHasta.SelectedValue)).IlacList.Add(cat);
            //collection.UpdateOne(Builders<doktor>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
            //                Builders<doktor>.Update.Set(b => b.ServisList, servislist));

            //var ilac = hastalist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue))?.IlacList ?? new List<ilac>();
            //dt = new System.Data.DataTable("DataTable");
            //ds = new System.Data.DataSet("DataSet");
            //dt.Columns.Add("ID").DefaultValue.ToString();
            //dt.Columns.Add("İlaç Adı").DefaultValue.ToString();
            //dt.Columns.Add("İlaç Gram").DefaultValue.ToString();
            //dt.Columns.Add("İlaç Adet").DefaultValue.ToString();
            //foreach (var item in ilac)
            //{
            //    string[] array = { item._id.ToString(), item.ilac_adi.ToString(), item.ilac_gram.ToString(), item.ilac_adet.ToString() };
            //    dt.Rows.Add(array);
            //}
            //ds.Tables.Add(dt);
            //GridView1.DataSource = ds;
            //GridView1.DataMember = "DataTable";
            //GridView1.DataBind();
        }

        //protected void ddlDoktor_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MongoClient client = new MongoClient();
        //    var database = client.GetDatabase("hastane");
        //    var collection = database.GetCollection<doktor>("doktorlistesi");
        //    var serviceList = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue) && x.ServisList.Count > 0).FirstOrDefault()?.ServisList ?? new List<servis>();
        //    ddlServis.DataSource = serviceList;
        //    ddlServis.DataBind();
        //}

        //protected void ddlServis_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MongoClient client = new MongoClient();
        //    var database = client.GetDatabase("hastane");
        //    var collection = database.GetCollection<doktor>("doktorlistesi");

        //    var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).FirstOrDefault()?.ServisList ?? new List<servis>();

        //    var hastalist = servislist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlServis.SelectedValue))?.HastaList ?? new List<hasta>();
        //    //var ilaclist = hastalist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue))?.IlacList ?? new List<ilac>();




        //    //var doctorfilter = Builders<doktor>.Filter.Eq("_id", ObjectId.Parse(ddlDoktor.SelectedValue));
        //    //var servicefilter = Builders<doktor>.Filter.And(doctorfilter, Builders<doktor>.Filter.ElemMatch(x => x.ServisList, x => x.HastaList));
        //    //var hastalistesi = collection.Find(servicefilter).ToList().Select(x => x.ServisList.Aggregate(x=>x.HastaList)).ToList();


        //    ddlHasta.DataSource = hastalist;
        //    ddlHasta.DataBind();

        //}

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName.CompareTo("Delete") == 0)
        //    {
        //        var index = Convert.ToInt32(e.CommandArgument);


        //        MongoClient client = new MongoClient();
        //        var database = client.GetDatabase("hastane");
        //        var collection = database.GetCollection<doktor>("doktorlistesi");
        //        var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).FirstOrDefault()?.ServisList ?? new List<servis>();
        //        var hastalist = servislist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlServis.SelectedValue))?.HastaList ?? new List<hasta>();
        //        var ilaclist = hastalist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue))?.IlacList ?? new List<ilac>();
        //        var filt = ilaclist.Find(x => x._id == ObjectId.Parse(GridView1.Rows[index].Cells[2].Text));
        //        ilaclist.Remove(filt);
        //        collection.UpdateOne(Builders<doktor>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
        //                    Builders<doktor>.Update.Set(b => b.ServisList, servislist));


        //        dt = new System.Data.DataTable("DataTable");
        //        ds = new System.Data.DataSet("DataSet");
        //        dt.Columns.Add("ID").DefaultValue.ToString();
        //        dt.Columns.Add("İlaç Adı").DefaultValue.ToString();
        //        dt.Columns.Add("İlaç Gram").DefaultValue.ToString();
        //        dt.Columns.Add("İlaç Adet").DefaultValue.ToString();
        //        foreach (var item in ilaclist)
        //        {
        //            string[] array = { item._id.ToString(), item.ilac_adi.ToString(), item.ilac_gram.ToString(), item.ilac_adet.ToString() };
        //            dt.Rows.Add(array);
        //        }
        //        ds.Tables.Add(dt);
        //        GridView1.DataSource = ds;
        //        GridView1.DataMember = "DataTable";
        //        GridView1.DataBind();



        //    }
        //    if (e.CommandName.CompareTo("Update") == 0)
        //    {
        //        var index = Convert.ToInt32(e.CommandArgument);
        //        MongoClient client = new MongoClient();
        //        var database = client.GetDatabase("hastane");
        //        var collection = database.GetCollection<doktor>("doktorlistesi");
        //        var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).FirstOrDefault()?.ServisList ?? new List<servis>();
        //        var hastalist = servislist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlServis.SelectedValue))?.HastaList ?? new List<hasta>();
        //        var ilaclist = hastalist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue))?.IlacList ?? new List<ilac>();
        //        var filt = ilaclist.Find(x => x._id == ObjectId.Parse(GridView1.Rows[index].Cells[2].Text));
        //        adıText.Value = filt.ilac_adi.ToString();
        //        adetText.Value = filt.ilac_adet.ToString();
        //        gramText.Value = filt.ilac_gram.ToString();
        //    }

        //}

        //protected void ddlHasta_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MongoClient client = new MongoClient();
        //    var database = client.GetDatabase("hastane");
        //    var collection = database.GetCollection<doktor>("doktorlistesi");
        //    var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).FirstOrDefault()?.ServisList ?? new List<servis>();
        //    var hastalist = servislist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlServis.SelectedValue))?.HastaList ?? new List<hasta>();
        //    var ilac = hastalist.FirstOrDefault(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue))?.IlacList ?? new List<ilac>();
        //    dt = new System.Data.DataTable("DataTable");
        //    ds = new System.Data.DataSet("DataSet");
        //    dt.Columns.Add("ID").DefaultValue.ToString();
        //    dt.Columns.Add("İlaç Adı").DefaultValue.ToString();
        //    dt.Columns.Add("İlaç Gram").DefaultValue.ToString();
        //    dt.Columns.Add("İlaç Adet").DefaultValue.ToString();
        //    foreach (var item in ilac)
        //    {
        //        string[] array = { item._id.ToString(), item.ilac_adi.ToString(), item.ilac_gram.ToString(), item.ilac_adet.ToString() };
        //        dt.Rows.Add(array);
        //    }
        //    ds.Tables.Add(dt);
        //    GridView1.DataSource = ds;
        //    GridView1.DataMember = "DataTable";
        //    GridView1.DataBind();
        //}
    }
}