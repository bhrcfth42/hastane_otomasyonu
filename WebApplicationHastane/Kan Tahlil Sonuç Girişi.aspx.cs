using iTextSharp.text;
using iTextSharp.text.pdf;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationHastane.Models;

namespace WebApplicationHastane
{
    public partial class Kan_Tahlil_Sonuç_Girişi : System.Web.UI.Page
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
                case "Laborant":
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
            var hastalist = database.GetCollection<yatanhastalar>("yatanhastalar").AsQueryable<yatanhastalar>().SelectMany(x => x.ServisList).SelectMany(x => x.HastaList).Select(k => new
            {
                AdSoyad = k.hasta_adi + " " + k.hasta_soyadi,
                Id = k._id
            }).ToList();
            ddlHasta.DataSource = hastalist;
            ddlHasta.DataBind();
            ddlHasta2.DataSource = hastalist;
            ddlHasta2.DataBind();
        }

        protected void ddlHasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var kanlist = database.GetCollection<yatanhastalar>("yatanhastalar").AsQueryable<yatanhastalar>().SelectMany(x => x.ServisList).SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).SelectMany(x => x.KanTahlilList).Select(k => new
            {
                Ad = k.Tarih,
                Id = k._id
            }).ToList();
            ddlKan.DataSource = kanlist;
            ddlKan.DataBind();
        }

        protected void ddlKan_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var tetkiklist = database.GetCollection<yatanhastalar>("yatanhastalar").AsQueryable<yatanhastalar>().SelectMany(x => x.ServisList).SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).SelectMany(x => x.KanTahlilList).Where(x => x._id == ObjectId.Parse(ddlKan.SelectedValue)).SelectMany(x => x.TahlilList).Select(k => new
            {
                Ad = k.tahlil_adi,
            }).ToList();
            ddlTetkik.DataSource = tetkiklist;
            ddlTetkik.DataBind();
        }

        protected void Kaydetbuton_Click(object sender, EventArgs e)
        {

            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var tetkiklist = database.GetCollection<yatanhastalar>("yatanhastalar").AsQueryable<yatanhastalar>().SelectMany(x => x.ServisList).SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).SelectMany(x => x.KanTahlilList).Where(x => x._id == ObjectId.Parse(ddlKan.SelectedValue)).ToList();
            var tetkik = tetkiklist.FirstOrDefault();
            var sonuc = database.GetCollection<islemsonuc>("sonuclistesi");
            var bul = sonuc.Find(x => x._id == tetkik._id).ToList();
            if (tetkiklist.Count != bul.Count)
            {
                islemsonuc cat = new islemsonuc();
                cat._id = tetkik._id;
                cat.Hasta_id = ObjectId.Parse(ddlHasta.SelectedValue);
                cat.tarih = tetkik.Tarih;
                cat.sonuc_cesidi = "Laboratuvar";
                sonuc.InsertOne(cat);
            }
            kansonuc cata = new kansonuc();
            cata.tahlil_adi = ddlTetkik.SelectedItem.Text;
            cata.sonuc = TextSonuc.Value;
            List<kansonuc> kanlist = sonuc.Find(x => x._id == ObjectId.Parse(ddlKan.SelectedValue)).FirstOrDefault()?.SonucList ?? new List<kansonuc>();
            kanlist.Add(cata);
            sonuc.UpdateOne(Builders<islemsonuc>.Filter.Eq(x => x._id, tetkik._id),
                            Builders<islemsonuc>.Update.Set(b => b.SonucList, kanlist));
            //var tetkiklist = database.GetCollection<yatanhastalar>("yatanhastalar").AsQueryable<yatanhastalar>().SelectMany(x => x.ServisList).SelectMany(x => x.HastaList).Where(x => x._id == ObjectId.Parse(ddlHasta.SelectedValue)).SelectMany(x => x.KanTahlilList).Where(x => x._id == ObjectId.Parse(ddlKan.SelectedValue)).SelectMany(x => x.TahlilList).Where(x => x._id == ObjectId.Parse(ddlTetkik.SelectedValue)).ToList();

        }

        protected void SonucYazdır_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var servislistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).ToList();
            var hastalistesi = servislistesi.SelectMany(x => x.HastaList).ToList().Where(x => x._id == ObjectId.Parse(ddlHasta2.SelectedValue)).ToList().FirstOrDefault();

            var sonuc = database.GetCollection<islemsonuc>("sonuclistesi").Find(x => x._id == ObjectId.Parse(ddlsonuctarih.SelectedValue) && x.sonuc_cesidi=="Laboratuvar").ToList().SelectMany(x => x.SonucList).ToList();



            #region Font seç
            BaseFont trArial = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\tahoma.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontArial = new iTextSharp.text.Font(trArial, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.DARK_GRAY);
            iTextSharp.text.Font fontArialHeader = new iTextSharp.text.Font(trArial, 13, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font fontArialbold = new iTextSharp.text.Font(trArial, 9, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.DARK_GRAY);
            iTextSharp.text.Font fontArialboldgeneral = new iTextSharp.text.Font(trArial, 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            #endregion

            #region Sonuç pdf oluştur
            iTextSharp.text.Document pdfFile = new iTextSharp.text.Document();
            PdfWriter.GetInstance(pdfFile, new FileStream("C:\\Users\\Önder\\Desktop\\Yeni klasör\\Kan Sonucu (" + hastalistesi.hasta_adi + " " + hastalistesi.hasta_soyadi + " " + ddlsonuctarih.SelectedItem.Text + ").pdf", FileMode.Create));
            pdfFile.Open();
            #endregion

            #region Sonuç oluşturan bilgileri
            pdfFile.AddCreator("Önder"); //Oluşturan kişinin isminin eklenmesi
            pdfFile.AddCreationDate();//Oluşturulma tarihinin eklenmesi
            pdfFile.AddAuthor("Laboratuvar"); //Yazarın isiminin eklenmesi
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
            pdfTableHeader.DefaultCell.Border = Rectangle.NO_BORDER;

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

            Paragraph yazi = new Paragraph("Hasta TC: " + hastalistesi.hasta_tc + "\nHasta Adı: " + hastalistesi.hasta_adi + "\nHasta Soyadı: " + hastalistesi.hasta_soyadi + "\n\n\n");


            #region Tabloyu Oluştur
            PdfPTable pdfTable = new PdfPTable(2);
            pdfTable.TotalWidth = 500f;
            pdfTable.LockedWidth = true;
            pdfTable.HorizontalAlignment = 1;
            pdfTable.DefaultCell.Padding = 5;
            pdfTable.DefaultCell.BorderColor = iTextSharp.text.BaseColor.GRAY;
            foreach (var item in sonuc)
            {
                pdfTable.AddCell(new Phrase(item.tahlil_adi, fontArialboldgeneral));
                pdfTable.AddCell(new Phrase(item.sonuc, fontArial));
            }
            #endregion

            #region Pdfe yaz ve dosyayı kapat
            if (pdfFile.IsOpen() == false) pdfFile.Open();
            pdfFile.Add(pdfTableHeader);
            pdfFile.Add(p);
            pdfFile.Add(yazi);
            pdfFile.Add(pdfTable);
            pdfFile.Close();
            #endregion

        }

        protected void ddlHasta2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var sonuc = database.GetCollection<islemsonuc>("sonuclistesi").AsQueryable<islemsonuc>().ToList().Select(x => new { Id = x._id, tarih = x.tarih }).ToList();
            ddlsonuctarih.DataSource = sonuc;
            ddlsonuctarih.DataBind();
        }
    }
}