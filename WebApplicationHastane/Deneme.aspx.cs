using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationHastane.Models;

namespace WebApplicationHastane
{
    public partial class Deneme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Obsolete]
        protected void Button1_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient();
            var database = client.GetDatabase("hastane");
            var collection = database.GetCollection<yatanhastalar>("yatanhastalar");
            var servislistesi = collection.Find(x => x._id != null).ToList().SelectMany(x => x.ServisList).ToList();
            var hastalistesi = servislistesi.SelectMany(x => x.HastaList).ToList();
            var radyolojilistesi = hastalistesi.SelectMany(x => x.RadyolojiList);
            var tetkiklistesi = radyolojilistesi.SelectMany(x => x.TetkiklerList).ToList();
            var hst = hastalistesi.FirstOrDefault();
            string name = FileUpload1.PostedFile.FileName;
            Book book = new Book()
            {
                Name = hst.hasta_adi + " " + hst.hasta_soyadi+" ("+ DateTime.UtcNow.ToString()+").jpg",
                Content = hst.hasta_radyoloji_durum,
                Data = File.ReadAllBytes(Convert.ToString(FileUpload1.PostedFile.FileName))
            };
            MongoClient client3 = new MongoClient("mongodb://localhost");
            MongoServer server3 = client3.GetServer();
            MongoDatabase database3 = server3.GetDatabase("test");
            MongoCollection collection3 = database3.GetCollection("docs");

            Stream memoryStream = new MemoryStream(book.Data);
            MongoGridFSFileInfo gfsi = database3.GridFS.Upload(memoryStream, book.Name);
            BsonDocument photoMetadata = new BsonDocument
                                         { { "FileName", FileUpload1.FileName },
                                         { "Type", FileUpload1.PostedFile.ContentType},
                                         { "Width", 3600 },
                                         { "Height", 4850 }};
            database3.GridFS.SetMetadata(gfsi, photoMetadata);
            book.ImageId = gfsi.Id.AsObjectId;
            collection3.Insert(book);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string dt = Request.Form[txtDate.UniqueID];
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Selected Date: " + dt + "');", true);
        }
    }
}