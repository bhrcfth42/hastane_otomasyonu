using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationHastane.Models;
using ZXing;

namespace WebApplicationHastane
{
    public partial class ServisHastaListeleme : System.Web.UI.Page
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
                case "Hemşire":
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
            var collection = database.GetCollection<servistek>("servislistesi");
            var servislist = collection.AsQueryable<servistek>().Select(k => new
            {
                Ad = k.servis_adi,
                ID = k._id
            }).ToList();
            ddlServis.DataSource = servislist;
            ddlServis.DataBind();
        }


        protected void ddlServis_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var doktorlistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).Where(x => x._id == ObjectId.Parse(ddlServis.SelectedValue)).ToList();
            var servislistesi = doktorlistesi.SelectMany(x => x.HastaList).ToList();


            //var hastalistesi = servislistesi.ServisList.FindAll(x => x._id == ObjectId.Parse(ddlServis.SelectedValue)).FirstOrDefault().HastaList;
            //var doktorlistesi = database.GetCollection<doktor>("doktorlistesi").AsQueryable<doktor>().FirstOrDefault().ServisList;
            //var servislistesi = doktorlistesi.AsQueryable().Where(x=>x._id == ObjectId.Parse(ddlServis.SelectedValue)).FirstOrDefault().HastaList;
            //var hastalistesi = servislistesi.AsQueryable();


            //    var hastalist = collection.Find().FirstOrDefault(x => x._id == ObjectId.Parse(ddlServis.SelectedValue))?.HastaList ?? new List<hasta>();


            dt = new System.Data.DataTable("DataTable");
            ds = new System.Data.DataSet("DataSet");
            dt.Columns.Add("ID").DefaultValue.ToString();
            dt.Columns.Add("Hasta TC").DefaultValue.ToString();
            dt.Columns.Add("Hasta Adı").DefaultValue.ToString();
            dt.Columns.Add("Hasta Soyadı").DefaultValue.ToString();
            dt.Columns.Add("Hasta Anne Adı").DefaultValue.ToString();
            dt.Columns.Add("Hasta Baba Adı").DefaultValue.ToString();
            dt.Columns.Add("Hasta Telefon Numarası").DefaultValue.ToString();
            dt.Columns.Add("Hasta Cinsiyet").DefaultValue.ToString();
            foreach (var item in servislistesi)
            {
                string[] array = { item._id.ToString(), item.hasta_tc.ToString(), item.hasta_adi.ToString(), item.hasta_soyadi.ToString(), item.hasta_anneadi.ToString(), item.hasta_babaadi.ToString(), item.hasta_telefon.ToString(), item.hasta_cinsiyet.ToString() };
                dt.Rows.Add(array);
            }
            ds.Tables.Add(dt);
            hastalisteGridView.DataSource = ds;
            hastalisteGridView.DataMember = "DataTable";
            hastalisteGridView.DataBind();
        }

        protected void hastalisteGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Select") == 0)
            {
                var index = Convert.ToInt32(e.CommandArgument);
                MongoClient client = new MongoClient();
                var database = client.GetDatabase("hastane");
                var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
                var doktorlistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).Where(x => x._id == ObjectId.Parse(ddlServis.SelectedValue));
                var servislistesi = doktorlistesi.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(hastalisteGridView.Rows[index].Cells[1].Text));
                var ilaclist = servislistesi.SelectMany(x => x.IlacList).ToList();
                var ilacistekdurum = servislistesi.Where(x => x.hasta_tedavi_durum == "Onaylandı").Select(x => x.hasta_tedavi_durum).FirstOrDefault();
                if (ilacistekdurum == "Onaylandı")
                {
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
                else if (ilacistekdurum == null)
                {
                    dt = new System.Data.DataTable("DataTable");
                    ds = new System.Data.DataSet("DataSet");
                    dt.Columns.Add("Durum").DefaultValue.ToString();
                    string yazi = "Tedavisi Bulunmamaktadır";
                    dt.Rows.Add(yazi);
                    ds.Tables.Add(dt);
                    ilaclisteleGridView1.DataSource = ds;
                    ilaclisteleGridView1.DataMember = "DataTable";
                    ilaclisteleGridView1.DataBind();
                }
                else
                {
                    dt = new System.Data.DataTable("DataTable");
                    ds = new System.Data.DataSet("DataSet");
                    string yazi = "Tedavi Eczaneden Onay Bekliyor";
                    dt.Columns.Add("Durum").DefaultValue.ToString();
                    dt.Rows.Add(yazi);
                    ds.Tables.Add(dt);
                    ilaclisteleGridView1.DataSource = ds;
                    ilaclisteleGridView1.DataMember = "DataTable";
                    ilaclisteleGridView1.DataBind();
                }
                ListBoxRadyoloji.Items.Clear();
                var radyolojiistekdurumu = servislistesi.Where(x => x.hasta_radyoloji_durum == "İstek Bulunmaktadır").Select(x => x.hasta_radyoloji_durum).FirstOrDefault();
                if (radyolojiistekdurumu == "İstek Bulunmaktadır")
                {
                    var radyoloji = servislistesi.Where(x => x.hasta_radyoloji_durum == "İstek Bulunmaktadır").SelectMany(x => x.RadyolojiList).ToList();
                    var radistek = radyoloji.SelectMany(x => x.TetkiklerList).ToList();

                    foreach (var item in radistek)
                    {
                        string rad = item.tahlil_adi;
                        ListBoxRadyoloji.Items.Add(rad);
                    }
                }
                else
                {
                    string yazi = "Radyoloji Tetkik İsteği Bulunamadı";
                    ListBoxRadyoloji.Items.Add(yazi);
                }
                var laboratuvaristekdurumu = servislistesi.Where(x => x.hasta_tahlil_durum == "İstek Bulunmaktadır").Select(x => x.hasta_tahlil_durum).FirstOrDefault();
                if (laboratuvaristekdurumu == "İstek Bulunmaktadır")
                {
                    var laboratuvar = servislistesi.Where(x => x.hasta_tahlil_durum == "İstek Bulunmaktadır").SelectMany(x => x.KanTahlilList).ToList();
                    var labistek = laboratuvar.SelectMany(x => x.TahlilList).ToList();
                    ListBoxKanTahlil.Items.Clear();
                    foreach (var item in labistek)
                    {
                        string lab = item.tahlil_adi;
                        ListBoxKanTahlil.Items.Add(lab);
                    }
                }
                else
                {
                    string yazi = "Kan Tahlil İsteği Bulunamadı";
                    ListBoxKanTahlil.Items.Add(yazi);
                }
            }
        }

        protected void ilaclisteleGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Select") == 0)
            {
                var index = Convert.ToInt32(e.CommandArgument);
                MongoClient client = new MongoClient();
                var database = client.GetDatabase("hastane");
                var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
                var doktorlistesi = collection.Find(x => x._id != null).ToList();
                var servislistesi = doktorlistesi.SelectMany(x => x.ServisList).ToList();
                var hastalistesi = servislistesi.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(hastalisteGridView.SelectedRow.Cells[1].Text)).Where(x => x.hasta_tedavi_durum == "Onaylandı");
                var ilaclist = hastalistesi.SelectMany(x => x.IlacList).ToList().Where(x => x._id == ObjectId.Parse(ilaclisteleGridView1.Rows[index].Cells[1].Text)).FirstOrDefault();
                if (ilaclist.ilac_uygulanacak_adet > 1)
                {
                    --ilaclist.ilac_uygulanacak_adet;
                    var ilaclistesi = hastalistesi.SelectMany(x => x.IlacList).ToList();
                    var filt = Builders<yatanhastalar>.Filter.ElemMatch(x => x.ServisList, Builders<servis>.Filter.ElemMatch(x => x.HastaList, Builders<hasta>.Filter.Eq(x => x._id, ObjectId.Parse(hastalisteGridView.SelectedRow.Cells[1].Text))));
                    var update = Builders<yatanhastalar>.Update.Set(x => x.ServisList, servislistesi);
                    collection.UpdateOne(filt, update);
                    //                collection.FindOneAndUpdate(c=>c.ServisList.Any(s => s._id == ObjectId.Parse(hastalisteGridView.SelectedRow.Cells[1].Text)), // find this match
                    //Builders<yatanhastalar>.Update.Set(c => c.ServisList[-1].HastaList[-1].IlacList, ilac));     // -1 means update first matching array element
                }
                else
                {
                    var servisId = ObjectId.Parse(ddlServis.SelectedValue);
                    var filt = Builders<yatanhastalar>.Filter.ElemMatch(x => x.ServisList, Builders<servis>.Filter.ElemMatch(x => x.HastaList, Builders<hasta>.Filter.ElemMatch(x => x.IlacList, Builders<ilac>.Filter.Eq(x => x._id, ObjectId.Parse(ilaclisteleGridView1.Rows[index].Cells[1].Text)))));
                    var update = Builders<yatanhastalar>.Update.Pull("ServisList.$[].HastaList.$[].IlacList", ilaclist);
                    collection.UpdateOne(filt, update);
                }

                dt = new System.Data.DataTable("DataTable");
                ds = new System.Data.DataSet("DataSet");
                dt.Columns.Add("ID").DefaultValue.ToString();
                dt.Columns.Add("İlaç Adı").DefaultValue.ToString();
                dt.Columns.Add("İlaç Barkod").DefaultValue.ToString();
                dt.Columns.Add("İlaç Uygulanacak Adet").DefaultValue.ToString();
                var ilaclist2 = hastalistesi.SelectMany(x => x.IlacList).ToList();
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
        }

        protected void Barkod_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var doktorlistesi = collection.Find(x => x._id != null).ToList();
            var servislistesi = doktorlistesi.SelectMany(x => x.ServisList).ToList();
            var hastalistesi = servislistesi.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(hastalisteGridView.SelectedRow.Cells[1].Text));
            foreach (var item in hastalistesi)
            {
                string hastabilgi = "Hasta TC: " + item.hasta_tc + "    Hasta Adı: " + item.hasta_adi + "    Hasta Soyadı: " + item.hasta_soyadi;
                for (int i = 0; i < ListBoxRadyoloji.Items.Count; i++)
                    hastabilgi += "\n" + ListBoxRadyoloji.Items[i].Text;
                var writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                var result = writer.Write(hastabilgi);
                string patch = Server.MapPath("~/image/qr.jpeg");
                var barcodebitmap = new Bitmap(result);

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(patch, FileMode.Create, FileAccess.ReadWrite))
                    {
                        barcodebitmap.Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Başarı ile Barkod Basıldı.');", true);
        }

        protected void BarkodKan_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var doktorlistesi = collection.Find(x => x._id != null).ToList();
            var servislistesi = doktorlistesi.SelectMany(x => x.ServisList).ToList();
            var hastalistesi = servislistesi.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(hastalisteGridView.SelectedRow.Cells[1].Text));
            var hst = hastalistesi.FirstOrDefault();
            string hastabilgi = "Hasta TC: " + hst.hasta_tc + "    Hasta Adı: " + hst.hasta_adi + "    Hasta Soyadı: " + hst.hasta_soyadi;
            for (int i = 0; i < ListBoxKanTahlil.Items.Count; i++)
                hastabilgi += "\n" + ListBoxKanTahlil.Items[i].Text;
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            var result = writer.Write(hastabilgi);
            string patch = Server.MapPath("~/image/a.jpeg");
            var barcodebitmap = new Bitmap(result);            
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(patch, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodebitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Başarı ile Barkod Basıldı.');", true);
        }
    }
}