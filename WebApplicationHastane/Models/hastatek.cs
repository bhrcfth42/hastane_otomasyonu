using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class hastatek
    {
        public ObjectId _id { get; set; }
        public Int64 hasta_tc { get; set; }
        public string hasta_adi { get; set; }
        public string hasta_soyadi { get; set; }
        public string hasta_anneadi { get; set; }
        public string hasta_babaadi { get; set; }
        public Int64 hasta_telefon { get; set; }
        public string hasta_adres { get; set; }
        public string hasta_cinsiyet { get; set; }
    }
}