using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class laboratuvar
    {
        public ObjectId _id { get; set; }
        public string Tetkik_alan_adi { get; set; }
        public List<tahlil> TahlilList { get; set; } = new List<tahlil>();
    }
    public class tahlil
    {
        public ObjectId _id { get; set; }
        public string tahlil_adi { get; set; }
    }
}