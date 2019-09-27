using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class islemsonuc
    {
        public ObjectId _id { get; set; }
        public ObjectId Hasta_id { get; set; }
        public string tarih { get; set; }
        public string sonuc_cesidi { get; set; }
        public List<kansonuc> SonucList { get; set; } = new List<kansonuc>();

    }
    public class kansonuc
    {
        public string tahlil_adi { get; set; }
        public string sonuc { get; set; }
    }
}