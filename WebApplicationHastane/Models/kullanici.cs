using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class kullanici
    {
        public ObjectId _id { get; set; }
        public string kullanici_adi { get; set; }
        public string parola { get; set; }
        public string yetki { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
    }
}