using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationHastane.Models
{
    public class yatanhastalar
    {
        public ObjectId _id { get; set; }
        public string doktor_adi { get; set; }
        public string doktor_soyadi { get; set; }
        public string doktor_bölüm { get; set; }
        public List<servis> ServisList { get; set; } = new List<servis>();
    }
    public class servis
    {
        public ObjectId _id { get; set; }
        public string servis_adi { get; set; }
        public List<hasta> HastaList { get; set; } = new List<hasta>();
    }
    public class hasta
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
        public string hasta_tedavi_durum { get; set; }
        public string hasta_radyoloji_durum { get; set; }
        public string hasta_tahlil_durum { get; set; }
        public List<ilac> IlacList { get; set; } = new List<ilac>();
        public List<radyolojitetkikler> RadyolojiList { get; set; } = new List<radyolojitetkikler>();
        public List<kantahliltetkikler> KanTahlilList { get; set; } = new List<kantahliltetkikler>();

    }
    public class ilac
    {
        public ObjectId _id { get; set; }
        public string ilac_adi { get; set; }
        public string barkod { get; set; }
        public string reçete_türü { get; set; }
        public Int32 ilac_uygulanacak_adet { get; set; }
    }
    public class radyolojitetkikler
    {
        public ObjectId _id { get; set; }
        public string tarih { get; set; }
        public List<tetkikler> TetkiklerList { get; set; } = new List<tetkikler>();
    }
    public class tetkikler
    {
        public string tahlil_adi { get; set; }
    }
    public class kantahliltetkikler
    {
        public ObjectId _id { get; set; }
        public string Tarih { get; set; }
        public List<kantahlil> TahlilList { get; set; } = new List<kantahlil>();
    }
    public class kantahlil
    {
        public string tahlil_adi { get; set; }
    }
}