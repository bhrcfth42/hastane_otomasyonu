using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class randevu
    {
        public ObjectId _id { get; set; }
        public string tarih { get; set; }
        public List<randevudoktor> DoktorList { get; set; } = new List<randevudoktor>();
    }
    public class randevudoktor
    {
        public ObjectId _id { get; set; }
        public string doktor_adi { get; set; }
        public string doktor_soyadi { get; set; }
        public string doktor_bölüm { get; set; }
        public List<randevuhasta> HastaList { get; set; } = new List<randevuhasta>();
    }
    public class randevuhasta
    {
        public Int64 tc_no { get; set; }
        public string hasta_adi { get; set; }
        public string hasta_soyadi { get; set; }
        public Int32 hasta_dogumyili { get; set; }
        public string hasta_telno { get; set; }
        public string saat { get; set; }
        public string randevu_alınma_zamanı { get; set; }
        public string epikriz { get; set; }
    }
}