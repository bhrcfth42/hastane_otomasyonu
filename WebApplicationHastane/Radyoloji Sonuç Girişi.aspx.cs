using iTextSharp.text;
using iTextSharp.text.pdf;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationHastane.Models;

namespace WebApplicationHastane
{
    public partial class Radyoloji_Sonuç_Girişi : System.Web.UI.Page
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
                case "Radyoloji":
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
            var servislistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).ToList();
            var hastalistesi = servislistesi.SelectMany(x => x.HastaList).ToList().Where(x => x.hasta_radyoloji_durum == "İstek Bulunmaktadır").Select(x => new
            {
                ID = x._id,
                AdSoyad = x.hasta_adi + " " + x.hasta_soyadi
            }).ToList();
            ddlHasta.DataSource = hastalistesi;
            ddlHasta.DataBind();
        }

        protected void ddlHasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var servislistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).ToList();
            var hastalistesi = servislistesi.SelectMany(x => x.HastaList).ToList().Where(x => x.hasta_radyoloji_durum == "İstek Bulunmaktadır" && x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            var radyolojilistesi = hastalistesi.SelectMany(x => x.RadyolojiList).ToList().Select(x => new
            {
                Id = x._id,
                Ad = x.tarih
            }).ToList();
            ddlRadyoloji.DataSource = radyolojilistesi;
            ddlRadyoloji.DataBind();
        }

        protected void ddlRadyoloji_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var servislistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).ToList();
            var hastalistesi = servislistesi.SelectMany(x => x.HastaList).ToList().Where(x => x.hasta_radyoloji_durum == "İstek Bulunmaktadır" && x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            var radyolojilistesi = hastalistesi.SelectMany(x => x.RadyolojiList).ToList().SelectMany(x => x.TetkiklerList).ToList();
            foreach (var item in radyolojilistesi)
            {
                ListBox1.Items.Add(item.tahlil_adi);
                ListBox1.SelectedIndex = -1;
            }
        }

        protected void btntetkikkaydet_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var servislistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).ToList();
            var hastalistesi = servislistesi.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).ToList();
            var radyolojilistesi = hastalistesi.SelectMany(x => x.RadyolojiList).ToList().Where(x => x._id == ObjectId.Parse(ddlRadyoloji.SelectedValue));
            var tetkiklistesi = radyolojilistesi.SelectMany(x => x.TetkiklerList).ToList().Where(x=>x.tahlil_adi==ListBox1.SelectedItem.Text);
            var hst = hastalistesi.FirstOrDefault();
            var rad = radyolojilistesi.FirstOrDefault();
            #region Font seç
            BaseFont trArial = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\tahoma.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontArial = new iTextSharp.text.Font(trArial, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.DARK_GRAY);
            iTextSharp.text.Font fontArialHeader = new iTextSharp.text.Font(trArial, 13, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font fontArialbold = new iTextSharp.text.Font(trArial, 9, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.DARK_GRAY);
            iTextSharp.text.Font fontArialboldgeneral = new iTextSharp.text.Font(trArial, 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            #endregion

            #region Sonuç pdf oluştur
            iTextSharp.text.Document pdfFile = new iTextSharp.text.Document();
            PdfWriter.GetInstance(pdfFile, new FileStream("C:\\Users\\Önder\\Desktop\\Yeni klasör\\Radyoloji Sonuç (" + hst.hasta_tc + " " + hst.hasta_adi + " " + hst.hasta_soyadi + " " + DateTime.UtcNow.ToShortDateString() + ").pdf", FileMode.Create));
            pdfFile.Open();
            #endregion

            #region Sonuç oluşturan bilgileri
            pdfFile.AddCreator("Önder"); //Oluşturan kişinin isminin eklenmesi
            pdfFile.AddCreationDate();//Oluşturulma tarihinin eklenmesi
            pdfFile.AddAuthor("Radyoloji"); //Yazarın isiminin eklenmesi
            pdfFile.AddHeader("Başlık", "PDF UYGULAMASI OLUSTUR");
            pdfFile.AddTitle("Sonuç Raporu"); //Başlık ve title eklenmesi
            #endregion

            #region Sonuç firma resmi ve tarihi oluştur
            iTextSharp.text.Image jpgimg = iTextSharp.text.Image.GetInstance(@"C:/Users/Önder/Desktop/Önder Fatih Buhurcu Staj Projesi/WebApplicationHastane/WebApplicationHastane/login/images/İsimsiz-1.png");
            jpgimg.ScalePercent(35);
            jpgimg.Alignment = iTextSharp.text.Image.LEFT_ALIGN;

            PdfPTable pdfTableHeader = new PdfPTable(3);
            pdfTableHeader.TotalWidth = 500f;
            pdfTableHeader.LockedWidth = true;
            //pdfTableHeader.DefaultCell.Border = Rectangle;

            PdfPCell cellheader1 = new PdfPCell(jpgimg);
            cellheader1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cellheader1.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
            cellheader1.FixedHeight = 60f;
            cellheader1.Border = 0;
            pdfTableHeader.AddCell(cellheader1);

            PdfPCell cellheader2 = new PdfPCell(new Phrase("SONUÇ RAPORU", fontArialHeader));
            cellheader2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellheader2.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cellheader2.FixedHeight = 60f;
            cellheader2.Border = 0;
            pdfTableHeader.AddCell(cellheader2);


            PdfPCell cellheader3 = new PdfPCell(new Phrase(DateTime.Now.ToShortDateString(), fontArial));
            cellheader3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cellheader3.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cellheader3.FixedHeight = 60f;
            cellheader3.Border = 0;
            pdfTableHeader.AddCell(cellheader3);
            #endregion

            Phrase p = new Phrase("\n");

            Paragraph yazi = new Paragraph("Hasta TC: " + hst.hasta_tc + "\nHasta Adı: " + hst.hasta_adi + "\nHasta Soyadı: " + hst.hasta_soyadi + "\n\n\n");


            #region Tabloyu Oluştur
            PdfPTable pdfTable = new PdfPTable(1);
            pdfTable.TotalWidth = 500f;
            pdfTable.LockedWidth = true;
            pdfTable.HorizontalAlignment = 1;
            pdfTable.DefaultCell.Padding = 5;
            pdfTable.DefaultCell.BorderColor = iTextSharp.text.BaseColor.GRAY;

            pdfTable.AddCell(new Phrase(ListBox1.SelectedItem.Text + " Sonucu", fontArialboldgeneral));
            pdfTable.AddCell(new Phrase(sonucText.Value, fontArial));

            #endregion

            #region Pdfe yaz ve dosyayı kapat
            if (pdfFile.IsOpen() == false) pdfFile.Open();
            pdfFile.Add(pdfTableHeader);
            pdfFile.Add(p);
            pdfFile.Add(yazi);
            pdfFile.Add(pdfTable);
            pdfFile.Close();
            #endregion

            var filter = Builders<yatanhastalar>.Filter.ElemMatch(x=>x.ServisList, Builders<servis>.Filter.ElemMatch(x=>x.HastaList, Builders<hasta>.Filter.And(Builders<hasta>.Filter.Eq(x=>x._id,ObjectId.Parse(ddlHasta.SelectedValue)), Builders<hasta>.Filter.ElemMatch(x=>x.RadyolojiList, Builders<radyolojitetkikler>.Filter.And(Builders<radyolojitetkikler>.Filter.Eq(x=>x._id,ObjectId.Parse(ddlRadyoloji.SelectedValue)), Builders<radyolojitetkikler>.Filter.ElemMatch(x=>x.TetkiklerList, Builders<tetkikler>.Filter.Eq(x=>x.tahlil_adi,ListBox1.SelectedItem.Text)))))));
            var update = Builders<yatanhastalar>.Update.Pull("ServisList.$[].HastaList.$[].RadyolojiList.$[].TetkiklerList", tetkiklistesi);
            collection.UpdateOne(filter, update);

            var sayı = radyolojilistesi.SelectMany(x => x.TetkiklerList).ToList();
            if (sayı.Count == 0)
            {
                var filter2 = Builders<yatanhastalar>.Filter.ElemMatch(x => x.ServisList, Builders<servis>.Filter.ElemMatch(x => x.HastaList, Builders<hasta>.Filter.Eq(x=>x._id,ObjectId.Parse(ddlHasta.SelectedValue))));
                hst.hasta_radyoloji_durum = "Sonuçlandı";
                var update2 = Builders<yatanhastalar>.Update.Set(b => b.ServisList, servislistesi);
            }
        }
    }
}