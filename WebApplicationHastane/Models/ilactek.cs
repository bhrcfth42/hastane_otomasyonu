using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class ilactek
    {
        public ObjectId _id { get; set; }
        public string ilac_adi { get; set; }
        public string barkod { get; set; }
        public string reçete_türü { get; set; }
        public Int32 ilac_toplam_adet { get; set; }
    }
}