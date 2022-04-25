using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using myProject.Models;
using myProject.ViewModel;

namespace myProject.Controllers
{
    public class ServisController : ApiController
    {
        DB01Entities db = new DB01Entities();
        SonucModel sonuc = new SonucModel();

        [HttpGet]
        [Route("api/dersliste")]
        public List<DersModel> DersListe()
        {
            List<DersModel> dersliste = db.Ders.Select(x => new DersModel()
            {
                dersId = x.dersId,
                dersAdi = x.dersAdi,
                dersKatId = x.dersKatId,
                dersKredi = x.dersKredi

            }).ToList();

            return dersliste;
        }

        [HttpGet]
        [Route("api/dersbyid/{dersId}")]
        public DersModel DersById(int dersId)
        {
            DersModel kayit = db.Ders.Where(s => s.dersId == dersId).Select(x=> new DersModel() {
                dersId = x.dersId,
                dersAdi = x.dersAdi,
                dersKatId = x.dersKatId,
                dersKredi = x.dersKredi
            }).FirstOrDefault();
            return kayit;
        }

        [HttpGet]
        [Route("api/odevliste")]
        public List<OdevModel> OdevListe() { 
            List<OdevModel> odevliste = db.Odev.Select(x => new OdevModel()
        {
            odevAdi = x.odevAdi,
            odevId = x.odevId,
            odevKatId = x.odevKatId
        }).ToList();

            return odevliste;
        }

        [HttpGet]
        [Route("api/odevbyid/{odevId}")]
        public OdevModel OdevById(int odevId)
        {
            OdevModel kayit = db.Odev.Where(s => s.odevId == odevId).Select(x => new OdevModel()
            {
                odevAdi = x.odevAdi,
                odevId = x.odevId,
                odevKatId = x.odevKatId
            }).FirstOrDefault();
            return kayit;
        }

        [HttpGet]
        [Route("api/ogrenciliste")]

        public List<OgrenciModel> OgrenciListe() {
            List<OgrenciModel> ogrenciliste = db.Ogrenci.Select(x => new OgrenciModel()
            {
                ogrAdi = x.ogrAdi,
                ogrAciklama = x.ogrAciklama,
                ogrId = x.ogrId,
                ogrKatId = x.ogrKatId,
                ogrNo = x.ogrNo,
                ogrSoyadi = x.ogrSoyadi
            }).ToList();
            return ogrenciliste;

        }

        [HttpGet]
        [Route("api/ogrbyid/{odevId}")]
        public OgrenciModel OgrenciById(int ogrId)
        {
            OgrenciModel kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).Select(x => new OgrenciModel()
            {
                ogrAdi = x.ogrAdi,
                ogrAciklama = x.ogrAciklama,
                ogrId = x.ogrId,
                ogrKatId = x.ogrKatId,
                ogrNo = x.ogrNo,
                ogrSoyadi = x.ogrSoyadi
            }).FirstOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/ogrenciekle")]

        public SonucModel OgrenciEkle(OgrenciModel model)
        {
            if (db.Ogrenci.Count(s => s.ogrNo== model.ogrNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Öğrenci Numarası Kayıtlıdır!";
                return sonuc;
            }
            Ogrenci yeni = new Ogrenci();
            yeni.ogrNo = model.ogrNo;
            yeni.ogrAciklama = model.ogrAciklama;
            yeni.ogrAdi = model.ogrAdi;
            yeni.ogrSoyadi = model.ogrSoyadi;
            db.Ogrenci.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Eklendi.";

            return sonuc;
        }

        [HttpPost]
        [Route("api/odevekle")]

        public SonucModel OdevEkle(OdevModel model)
        {
            if (db.Odev.Count(s => s.odevAdi == model.odevAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ödev Adı Kayıtlıdır!";
                return sonuc;
            }

            Odev yeni = new Odev();
            yeni.odevAdi = model.odevAdi;
            db.Odev.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ödev Eklendi.";

            return sonuc;
        }

        [HttpPost]
        [Route("api/dersekle")]

        public SonucModel DersEkle(DersModel model)
        {
            if (db.Ders.Count(s => s.dersAdi== model.dersAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ders Adı Kayıtlıdır!";
                return sonuc;
            }

            Ders yeni = new Ders();
            yeni.dersAdi = model.dersAdi;
            yeni.dersKredi = model.dersKredi;
            db.Ders.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ders eklendi.";

            return sonuc;
        }

        [HttpPut]
        [Route("api/odevduzenle")]

        public SonucModel OdevDuzenle(OdevModel model)
        {
            Odev kayit = db.Odev.Where(s => s.odevId == model.odevId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödev Bulunamadı!";
                return sonuc;
            }

            kayit.odevAdi = model.odevAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ödev Düzenlendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/dersduzenle")]

        public SonucModel DersDuzenle(DersModel model)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == model.dersId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunamadı!";
                return sonuc;
            }

            kayit.dersAdi = model.dersAdi;
            kayit.dersKredi = model.dersKredi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ders Düzenlendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/ogrenciduzenle")]

        public SonucModel OgrenciDuzenle(OgrenciModel model)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == model.ogrId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Bulunamadı!";
                return sonuc;
            }

            kayit.ogrAciklama = model.ogrAciklama;
            kayit.ogrAdi = model.ogrAdi;
            kayit.ogrNo = model.ogrNo;
            kayit.ogrSoyadi = model.ogrSoyadi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/odevsil/{odevId}")]

        public SonucModel OdevSil(int odevId)
        {
            Odev kayit = db.Odev.Where(s => s.odevId == odevId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödev Bulunamadı!";
                return sonuc;
            }

            db.Odev.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ödev Silindi.";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/derssil/{dersId}")]

        public SonucModel DersSil(int dersId)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == dersId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunamadı!";
                return sonuc;
            }

            db.Ders.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ders Silindi.";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/ogrencisil/{ogrId}")]

        public SonucModel OgrenciSil(int ogrId)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Bulunamadı!";
                return sonuc;
            }

            db.Ogrenci.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi.";
            return sonuc;
        }

    }
}
