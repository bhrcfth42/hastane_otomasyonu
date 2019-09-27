using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class servistek
    {
        public ObjectId _id { get; set; }
        public string servis_adi { get; set; }
    }
}