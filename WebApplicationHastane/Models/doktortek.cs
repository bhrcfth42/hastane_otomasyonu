using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class doktortek
    {
        public ObjectId _id { get; set; }
        public string doktor_adi { get; set; }
        public string doktor_soyadi { get; set; }
        public string doktor_bölüm { get; set; }
    }
}