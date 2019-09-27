using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationHastane.Models;

namespace WebApplicationHastaneOtomasyonu
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
        }

        protected void loginbutton_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var kullanıcılar = database.GetCollection<kullanici>("kullanicilar");
            var username = textuser.Value;
            var password = textpassword.Value;
            var kullanıcı = kullanıcılar.Find(x => x.kullanici_adi == username&&x.parola==password).ToList().FirstOrDefault();
            if (kullanıcı != null)
            {
                Session.Add("adi", kullanıcı.isim);
                Session.Add("soyadi", kullanıcı.soyisim);
                Response.Redirect("AnaSayfa.aspx?yetki=" + kullanıcı.yetki);                
            }
               
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Kullanıcı Adı Veya Parola Yanlış Girildi.');", true);      
        }
    }
}