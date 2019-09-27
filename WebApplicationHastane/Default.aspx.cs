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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
        }

        protected void gönder_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<sikayetler>("sikayetler");
            sikayetler cat = new sikayetler();
            cat.ad_soyad = isim.Value;
            cat.mail_adress = email.Value;
            cat.telefon_no = phoneNumber.Value;
            cat.konu = konu.Value;
            cat.mesaj = mesaj.Value;
            cat.tarih = DateTime.UtcNow.ToShortDateString();
            if(cat.ad_soyad=="" || cat.konu=="" || cat.mesaj == "" || cat.telefon_no == "" || cat.mail_adress == "" || cat._id == null)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Mesajınız Gönderilemedi. Lütfen Boş Kısımlar Bırakmadığınızdan Emin Olunuz.');", true);
            else
            {
                collection.InsertOne(cat);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Mesajınız Başarıyla Gönderildi.');", true);
            }
            
        }
    }
}