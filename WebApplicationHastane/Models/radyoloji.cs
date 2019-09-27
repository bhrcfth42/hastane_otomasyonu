using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace WebApplicationHastane.Models
{
    public class radyoloji
    {
        public ObjectId _id { get; set; }
        public string radyoloji_alan_adi { get; set; }
        public List<tetkik> TetkikList { get; set; } = new List<tetkik>();
    }
    public class tetkik
    {
        public ObjectId _id { get; set; }
        public string tetkik_adi { get; set; }
    }
}