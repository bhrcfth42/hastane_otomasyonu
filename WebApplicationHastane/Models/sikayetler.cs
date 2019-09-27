using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class sikayetler
    {
        public ObjectId _id { get; set; }
        public string ad_soyad { get; set; }
        public string mail_adress { get; set; }
        public string telefon_no { get; set; }
        public string konu { get; set; }
        public string mesaj { get; set; }
        public string tarih { get; set; }
        public string sonuc { get; set; }
    }
}