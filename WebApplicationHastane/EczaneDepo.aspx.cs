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
    public partial class EczaneDepo : System.Web.UI.Page
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
            var ilaccol = database.GetCollection<ilactek>("ilaclistesi").AsQueryable<ilactek>();
            var ilaclist = ilaccol.ToList().Select(k => new
            {
                AdGram = k.ilac_adi,
                ID = k._id
            }).ToList();
            ddlİlaç.DataSource = ilaclist;
            ddlİlaç.DataBind();
            System.Data.DataTable dt = new System.Data.DataTable("DataTable");
            System.Data.DataSet ds = new System.Data.DataSet("DataSet");
            dt = new System.Data.DataTable("DataTable");
            ds = new System.Data.DataSet("DataSet");
            dt.Columns.Add("ID").DefaultValue.ToString();
            dt.Columns.Add("İlaç Adı").DefaultValue.ToString();
            dt.Columns.Add("İlaç Barkod").DefaultValue.ToString();
            dt.Columns.Add("İlaç Reçete Türü").DefaultValue.ToString();
            dt.Columns.Add("İlaç Adet").DefaultValue.ToString();
            foreach (var item in ilaccol)
            {
                string[] array = { item._id.ToString(), item.ilac_adi.ToString(), item.barkod.ToString(), item.reçete_türü.ToString(), item.ilac_toplam_adet.ToString() };
                dt.Rows.Add(array);
            }
            ds.Tables.Add(dt);
            ilaclisteleGridView1.DataSource = ds;
            ilaclisteleGridView1.DataMember = "DataTable";
            ilaclisteleGridView1.DataBind();
        }

        protected void ddlİlaç_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var ilaccol = database.GetCollection<ilactek>("ilaclistesi").AsQueryable<ilactek>();
            var ilaclist = ilaccol.Where(x => x._id == ObjectId.Parse(ddlİlaç.SelectedValue)).ToList();
            System.Data.DataTable dt = new System.Data.DataTable("DataTable");
            System.Data.DataSet ds = new System.Data.DataSet("DataSet");
            dt = new System.Data.DataTable("DataTable");
            ds = new System.Data.DataSet("DataSet");
            dt.Columns.Add("ID").DefaultValue.ToString();
            dt.Columns.Add("İlaç Adı").DefaultValue.ToString();
            dt.Columns.Add("İlaç Barkod").DefaultValue.ToString();
            dt.Columns.Add("İlaç Reçete Türü").DefaultValue.ToString();
            dt.Columns.Add("İlaç Adet").DefaultValue.ToString();
            foreach (var item in ilaclist)
            {
                string[] array = { item._id.ToString(), item.ilac_adi.ToString(), item.barkod.ToString(), item.reçete_türü.ToString(), item.ilac_toplam_adet.ToString() };
                dt.Rows.Add(array);
            }
            ds.Tables.Add(dt);
            ilaclisteleGridView2.DataSource = ds;
            ilaclisteleGridView2.DataMember = "DataTable";
            ilaclisteleGridView2.DataBind();
        }

        protected void ilaclisteleGridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ilaclisteleGridView1.PageIndex = e.NewPageIndex;
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var ilaccol = database.GetCollection<ilactek>("ilaclistesi").AsQueryable<ilactek>();
            System.Data.DataTable dt = new System.Data.DataTable("DataTable");
            System.Data.DataSet ds = new System.Data.DataSet("DataSet");
            dt = new System.Data.DataTable("DataTable");
            ds = new System.Data.DataSet("DataSet");
            dt.Columns.Add("ID").DefaultValue.ToString();
            dt.Columns.Add("İlaç Adı").DefaultValue.ToString();
            dt.Columns.Add("İlaç Barkod").DefaultValue.ToString();
            dt.Columns.Add("İlaç Reçete Türü").DefaultValue.ToString();
            dt.Columns.Add("İlaç Adet").DefaultValue.ToString();
            foreach (var item in ilaccol)
            {
                string[] array = { item._id.ToString(), item.ilac_adi.ToString(), item.barkod.ToString(), item.reçete_türü.ToString(), item.ilac_toplam_adet.ToString() };
                dt.Rows.Add(array);
            }
            ds.Tables.Add(dt);
            ilaclisteleGridView1.DataSource = ds;
            ilaclisteleGridView1.DataMember = "DataTable";
            ilaclisteleGridView1.DataBind();
        }
    }
}