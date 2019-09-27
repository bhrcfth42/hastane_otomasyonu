using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationHastane.tckimlik;

namespace WebApplicationHastane
{
    public partial class Hasta_Randevu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPSPublicSoapClient sorgula = new KPSPublicSoapClient();
            long tckimlik = Convert.ToInt64(tckimlikno.Value);
            var sonuc = sorgula.TCKimlikNoDogrula(tckimlik, Ad.Value.ToUpper(), Soyad.Value.ToUpper(), Convert.ToInt32(birthDate.Value));
            if (sonuc&&phoneNumber.Value!=null)
            {
                Session.Add("tc", tckimlik);
                Session.Add("ad", Ad.Value.ToUpper());
                Session.Add("soyad", Soyad.Value.ToUpper());
                Session.Add("yıl", Convert.ToInt32(birthDate.Value));
                Session.Add("telno", phoneNumber.Value);
                Response.Redirect("Hasta Randevu Paneli.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Message", "alert('Doğrulama Yanlış.');", true);
            }
        }
    }
}