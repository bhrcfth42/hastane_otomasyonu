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
    public partial class Kayıt : System.Web.UI.Page
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

            if (IsPostBack)
                return;
        }

        protected void loginbutton_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var kullanıcılar = database.GetCollection<kullanici>("kullanicilar");
            kullanici cat = new kullanici();
            cat.isim = textadi.Value;
            cat.soyisim = textsoyadi.Value;
            cat.yetki = ddlyetki.SelectedItem.Text;
            cat.kullanici_adi = textuser.Value;
            if (textpassword.Value != textpassword2.Value)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Parolalar Eşleşmiyor.');", true);
            else
            {
                cat.parola = textpassword.Value;
                kullanıcılar.InsertOne(cat);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Kayıt İşlemi Yapıldı.');", true);
            }
               
        }
    }
}