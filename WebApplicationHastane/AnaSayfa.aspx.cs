using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationHastane
{
    public partial class AnaSayfa : System.Web.UI.Page
    {
        string yetki;
        protected void hastakayit_Click(object sender, EventArgs e)
        {
            
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Sekreter")
                Response.Redirect("SahipsizHastaEkle.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void hastabilgigüncelle_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Sekreter")
                Response.Redirect("HastaBilgiGüncelle.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void hastayatisislemleri_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Doktor" || yetki == "Sekreter")
                Response.Redirect("HastaYatış.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }
        protected void hastatrasnfer_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Sekreter")
                Response.Redirect("HastaTransfer.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }
        protected void ilackaydet_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Eczacı")
                Response.Redirect("İlaçEkle.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }
        protected void ilacdepo_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin"|| yetki == "Eczacı")
                Response.Redirect("EczaneDepo.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }
        protected void doktorekle_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin")
                Response.Redirect("Doktor Ekle.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void tedaviyazdir_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Doktor")
                Response.Redirect("Tedavi.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }
        protected void servishastalisteleme_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Hemşire")
                Response.Redirect("ServisHastaListeleme.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }
        protected void servisekle_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin")
                Response.Redirect("ServisEkleme.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            if (Request.QueryString.Count == 0|| Request.QueryString.Get("yetki") == null)
                Response.Redirect("Login.aspx");
            else
                yetki = Request.QueryString["yetki"].ToString();
            Label1.Text = Session["adi"] + " " + Session["soyadi"];
        }

        protected void kantahlilistek_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Doktor")
                Response.Redirect("Kan Tahlil İstek.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void radyolojiistek_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Doktor")
                Response.Redirect("Radyoloji İstek.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void eczaneilaconay_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Eczacı")
                Response.Redirect("Eczane İlaç Onay.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void kantahlilsonuc_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Laborant")
                Response.Redirect("Kan Tahlil Sonuç Girişi.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void kantahliltetkikkayıt_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Laborant")
                Response.Redirect("Kan Tahlil Tetkik Kayıt.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void radyolojitetkikkayıt_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Radyoloji")
                Response.Redirect("Radyoloji Tetkik Kayıt.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void radyolojisonucgirisi_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Radyoloji")
                Response.Redirect("Radyoloji Sonuç Girişi.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void gönderilmismesajlar_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin")
                Response.Redirect("Şikayet Görüntüleme.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }

        protected void randevuhasta_Click(object sender, EventArgs e)
        {
            yetki = Request.QueryString["yetki"].ToString();
            if (yetki == "Admin" || yetki == "Doktor")
                Response.Redirect("Randevulu Hasta Sonuç.aspx?yetki=" + yetki + "");
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Yetkiniz Bulunmamaktadır.');", true);
        }
    }
}