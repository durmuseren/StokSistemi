using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;//modelsi tanımladık
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities(); //db nesne.mvcstokentitiesten türettik.Tablolara ulaşmak için db nesnesine ihtiyacımız var.
        public ActionResult Index(int sayfa=1) //sayfalama için değişken tanımladık.
        {
            //  var degerler = db.TBLKATEGORILER.ToList(); //tbl kategoriler içinde bulunan değerleri bana listele
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 4);//KAçıncı sayfadan başlasın 1,kaç tane olsun bir sayfada 4 
            return View(degerler); //bana geriye degerleri döndür.
        }
        [HttpGet] //Sayfada herhangi bir işlem yapılmıyorsa sadece görüünümü geri döndür.
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost] //sayfa işlem yapıldığında aşağıdaki kodları çalıştır.Mesela butona tıklandığında vs.
        public ActionResult YeniKategori(TBLKATEGORILER p1) //p1 parametre
        {
            if(!ModelState.IsValid)// Doğrulama işlemi yapılmadıysa
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);//p1den gelen değerleri eklep1in değerleri viewden gelcek
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id) //silme işlemi.ilişkili kategoridekiler silinmez
        {
            var kategori = db.TBLKATEGORILER.Find(id); //kategoriler içinde bul
            db.TBLKATEGORILER.Remove(kategori);//sil
            db.SaveChanges();//değişiklikleri kaydet
            return RedirectToAction("Index");//index sayfasına yönlendir
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);//kategori getir içinde bize ktgryi getirsin.
        }
        public ActionResult Guncelle(TBLKATEGORILER p1)//parametreye göre güncelliycez
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);//pden göndermiş olduğum kategorinin idsini bul
            ktg.KATEGORIAD = p1.KATEGORIAD;//
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}