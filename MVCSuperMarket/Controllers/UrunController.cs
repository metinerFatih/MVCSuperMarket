using Microsoft.AspNetCore.Mvc;
using MVCSuperMarket.Classes;
using MVCSuperMarket.Models;

namespace MVCSuperMarket.Controllers
{
    public class UrunController : Controller
    {
        private readonly SuperMarketDbContext _db;
        public UrunController(SuperMarketDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var urunler = _db.Urunler.ToList();
            return View(urunler);
        }

        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(UrunViewModel vm)
        {
            //Formdan gelen UrunViewModel nesnesini yakalayıp, özelliklerini hakiki urun'e atayıp DB'ye ekleyeceğiz.
            //Urun eklerken kişi resim eklemek zorunda değildir. Resim eklendiyse resimle ilgili işlemleri yap dememiz yeterli.
            Urun urun = new Urun();
            try
            {
                if (vm.Fiyat < 0)
                    throw new Exception("Geçersiz Fiyat");
                if (_db.Urunler.Count(u => u.Ad.ToLower().Trim() == vm.Ad.ToLower().Trim()) > 0)
                    throw new Exception($"{vm.Ad} adında bir ürün mevcuttur. Lütfen başka bir ürün girişi yapınız.");

                if (vm.Resim is not null)
                {
                    var dosyaAdi = vm.Resim.FileName;

                    // Dosyanın kaydedileceği konum belirlenir.
                    var konum = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler", dosyaAdi);
                    // Sonra dosya için bir akış ortamı oluşturulur. Kaydet için ortam hazırlıyoruz.

                    var akisOrtami = new FileStream(konum, FileMode.Create);

                    //Resmi kaydet
                    vm.Resim.CopyTo(akisOrtami);
                    //Başka durumlarda problem almamak için akış ortamını kapatmalıyız.
                    akisOrtami.Close();

                    urun.Resim = dosyaAdi;
                }
            }
            catch (Exception ex)
            {
                TempData["Durum"] = "Hata Oluştu! " + ex.Message;
                return View();
            }

            if (ModelState.IsValid)
            {
                urun.Ad = vm.Ad;
                urun.Fiyat = vm.Fiyat;
                urun.StoktaVarMi = vm.StoktaVarMi;

                _db.Urunler.Add(urun);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Duzenle(int id)
        {
            var urun = _db.Urunler.SingleOrDefault(x => x.Id == id);
            if (urun == null)
                return NotFound();

            UrunViewModel vm = new UrunViewModel()
            {
                Ad = urun.Ad,
                Fiyat = urun.Fiyat,
                StoktaVarMi = urun.StoktaVarMi
            };
            TempData["urun"] = id;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Duzenle(UrunViewModel vm)
        {
            int id = Convert.ToInt32(TempData["urun"]);
            var urun = _db.Urunler.SingleOrDefault(x => x.Id == id);
            if (urun is not null && ModelState.IsValid)
            {
                if (vm.Resim is not null)
                {
                    var dosyaAdi = vm.Resim.FileName;

                    // Dosyanın kaydedileceği konum belirlenir.
                    var konum = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler", dosyaAdi);
                    // Sonra dosya için bir akış ortamı oluşturulur. Kaydet için ortam hazırlıyoruz.

                    var akisOrtami = new FileStream(konum, FileMode.Create);

                    //Resmi kaydet
                    vm.Resim.CopyTo(akisOrtami);
                    //Başka durumlarda problem almamak için akış ortamını kapatmalıyız.
                    akisOrtami.Close();

                    urun.Resim = dosyaAdi;
                }
                urun.Ad = vm.Ad;
                urun.StoktaVarMi = vm.StoktaVarMi;
                urun.Fiyat = vm.Fiyat;
                urun.GuncellenmeTarihi = DateTime.Now;
                _db.Urunler.Update(urun);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Sil(int id)
        {
            var urun = _db.Urunler.SingleOrDefault(x => x.Id == id);
            if (urun == null)
                return NotFound();
            TempData["Id"] = id;
            return View(urun);
        }
        [HttpPost]
        public IActionResult Sil(Urun urun)
        {
            urun = _db.Urunler.SingleOrDefault(x => x.Id == (int)TempData["Id"]);
            bool resimKullaniciSayisiBirdenFazla = _db.Urunler.Count(x => x.Resim == urun.Resim) > 1 ? true : false;
            if (!resimKullaniciSayisiBirdenFazla)
            {
                //string[] resimAdları = Directory.GetFiles("C:\\Users\\monst\\Desktop\\dotNet\\MVCSuperMarket\\MVCSuperMarket\\wwwroot\\Resimler\\");
                var resimAdları = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler"));

                var dosyaIsmi = resimAdları.SingleOrDefault(x => x.Contains(urun.Resim));

                System.IO.File.Delete(dosyaIsmi);
            }
            _db.Urunler.Remove(urun);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ResimKaldir(int id)
        {
            var urun = _db.Urunler.Find(id);

            bool resimKullaniciSayisiBirdenFazla = _db.Urunler.Count(x => x.Resim == urun.Resim) > 1 ? true : false;
            if (!resimKullaniciSayisiBirdenFazla)
            {
                //string[] resimAdları = Directory.GetFiles("C:\\Users\\monst\\Desktop\\dotNet\\MVCSuperMarket\\MVCSuperMarket\\wwwroot\\Resimler\\");
                var resimAdları = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler"));

                var dosyaIsmi = resimAdları.SingleOrDefault(x => x.Contains(urun.Resim));

                System.IO.File.Delete(dosyaIsmi);
            }

            urun.Resim = null!;
            urun.GuncellenmeTarihi = DateTime.Now;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
