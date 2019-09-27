using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebApplicationHastane.Models;

namespace WebApplicationHastane
{
    public partial class Tedavi : System.Web.UI.Page
    {
        System.Data.DataTable dt = new System.Data.DataTable("DataTable");
        System.Data.DataSet ds = new System.Data.DataSet("DataSet");
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
            var doktorlist = database.GetCollection<doktortek>("doktorlistesi").AsQueryable<doktortek>().Select(k => new
            {
                AdSoyad = k.doktor_adi + " " + k.doktor_soyadi,
                ID = k._id
            }).ToList();
            ddlDoktor.DataSource = doktorlist;
            ddlDoktor.DataBind();


            var ilaccol = database.GetCollection<ilactek>("ilaclistesi").AsQueryable<ilactek>();
            var ilaclist = ilaccol.ToList().Select(k => new
            {
                AdGram = k.ilac_adi,
                ID = k._id
            }).ToList();
            ddlİlaç.DataSource = ilaclist;
            ddlİlaç.DataBind();
        }

        protected void btnilackaydet_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList().SelectMany(x => x.ServisList);
            var hastalist = servislist.SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            var ilc = hastalist.SelectMany(x => x.IlacList).Where(x => x._id == ObjectId.Parse(ddlİlaç.SelectedValue)).ToList();
            var ilaclistesi = database.GetCollection<ilactek>("ilaclistesi").Find(x => x._id == ObjectId.Parse(ddlİlaç.SelectedValue)).ToList();
            if (ilc.Count != ilaclistesi.Count)
            {
                foreach (var item in ilaclistesi)
                {
                    ilac cat = new ilac();
                    cat._id = item._id;
                    cat.ilac_adi = item.ilac_adi;
                    cat.barkod = item.barkod;
                    cat.ilac_uygulanacak_adet = Convert.ToInt32(adetText.Value);
                    List<ilac> ilaclist = hastalist.Find(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue))?.IlacList ?? new List<ilac>();
                    hastalist.FirstOrDefault(k => k._id == ObjectId.Parse(ddlHasta.SelectedValue)).IlacList.Add(cat);
                    var id = ObjectId.Parse(ddlHasta.SelectedValue);
                    collection.UpdateOne(Builders<yatanhastalar>.Filter.Eq(x => x._id, ObjectId.Parse(ddlDoktor.SelectedValue)),
                                    Builders<yatanhastalar>.Update.Set(b => b.ServisList, servislist));
                }
                var ilaccol = database.GetCollection<ilactek>("ilaclistesi");
                var ilacfilt = Builders<ilactek>.Filter.Eq("_id", ObjectId.Parse(ddlİlaç.SelectedValue));
                var ilacadetfilt = ilaccol.Find(x => x._id == ObjectId.Parse(ddlİlaç.SelectedValue)).ToList().Select(x => x.ilac_toplam_adet).FirstOrDefault();
                ilacadetfilt -= Convert.ToInt32(adetText.Value);
                var ilacupdate = Builders<ilactek>.Update.Set(x => x.ilac_toplam_adet, ilacadetfilt);
                ilaccol.UpdateOne(ilacfilt, ilacupdate);
            }
            var doktorlistesi = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList().SelectMany(x => x.ServisList);
            var servislistesi = doktorlistesi.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue));
            var ilaclist2 = servislistesi.SelectMany(x => x.IlacList).ToList();
            dt = new System.Data.DataTable("DataTable");
            ds = new System.Data.DataSet("DataSet");
            dt.Columns.Add("ID").DefaultValue.ToString();
            dt.Columns.Add("İlaç Adı").DefaultValue.ToString();
            dt.Columns.Add("İlaç Barkod").DefaultValue.ToString();
            dt.Columns.Add("İlaç Uygulanacak Adet").DefaultValue.ToString();
            foreach (var item in ilaclist2)
            {
                string[] array = { item._id.ToString(), item.ilac_adi.ToString(), item.barkod.ToString(), item.ilac_uygulanacak_adet.ToString() };
                dt.Rows.Add(array);
            }
            ds.Tables.Add(dt);
            ilaclisteleGridView1.DataSource = ds;
            ilaclisteleGridView1.DataMember = "DataTable";
            ilaclisteleGridView1.DataBind();
        }

        protected void ddlDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList().SelectMany(x => x.ServisList);
            var hastalist = servislist.SelectMany(x => x.HastaList).Select(k => new { AdSoyad = k.hasta_adi + " " + k.hasta_soyadi, ID = k._id });
            ddlHasta.DataSource = hastalist;
            ddlHasta.DataBind();
        }

        protected void ddlHasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var doktorlistesi = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList().SelectMany(x => x.ServisList);
            var servislistesi = doktorlistesi.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue));
            var ilaclist = servislistesi.SelectMany(x => x.IlacList).ToList();
            dt = new System.Data.DataTable("DataTable");
            ds = new System.Data.DataSet("DataSet");
            dt.Columns.Add("ID").DefaultValue.ToString();
            dt.Columns.Add("İlaç Adı").DefaultValue.ToString();
            dt.Columns.Add("İlaç Barkod").DefaultValue.ToString();
            dt.Columns.Add("İlaç Uygulanacak Adet").DefaultValue.ToString();
            foreach (var item in ilaclist)
            {
                string[] array = { item._id.ToString(), item.ilac_adi.ToString(), item.barkod.ToString(), item.ilac_uygulanacak_adet.ToString() };
                dt.Rows.Add(array);
            }
            ds.Tables.Add(dt);
            ilaclisteleGridView1.DataSource = ds;
            ilaclisteleGridView1.DataMember = "DataTable";
            ilaclisteleGridView1.DataBind();
        }

        protected void ilaclisteleGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Select") == 0)
            {
                var index = Convert.ToInt32(e.CommandArgument);
                MongoClient client = new MongoClient();
                var database = client.GetDatabase("hastane");
                var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
                var servislist = collection.Find(x => x._id == ObjectId.Parse(ddlDoktor.SelectedValue)).ToList().SelectMany(x => x.ServisList);
                var hastalist = servislist.SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
                var ilc = hastalist.SelectMany(x => x.IlacList).ToList().Where(x => x._id == ObjectId.Parse(ilaclisteleGridView1.Rows[index].Cells[1].Text)).FirstOrDefault();
                var doktorId = ObjectId.Parse(ddlDoktor.SelectedValue);
                var filter = Builders<yatanhastalar>.Filter.Eq(k => k._id, doktorId);
                var update = Builders<yatanhastalar>.Update.Pull("ServisList.$[].HastaList.$[].IlacList", ilc);
                collection.UpdateOne(filter, update);
                var ilaccol = database.GetCollection<ilactek>("ilaclistesi");
                var ilacfilt = Builders<ilactek>.Filter.Eq("_id", ObjectId.Parse(ilaclisteleGridView1.Rows[index].Cells[1].Text));
                var ilacadetfilt = ilaccol.Find(x => x._id == ObjectId.Parse(ilaclisteleGridView1.Rows[index].Cells[1].Text)).ToList().Select(x => x.ilac_toplam_adet).FirstOrDefault();
                ilacadetfilt += Convert.ToInt32(ilaclisteleGridView1.Rows[index].Cells[4].Text);
                var ilacupdate = Builders<ilactek>.Update.Set(x => x.ilac_toplam_adet, ilacadetfilt);
                ilaccol.UpdateOne(ilacfilt, ilacupdate);

            }
        }
    }
}