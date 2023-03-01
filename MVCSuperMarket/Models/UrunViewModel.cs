namespace MVCSuperMarket.Models
{
    public class UrunViewModel
    {
        public string Ad { get; set; }
        public bool StoktaVarMi { get; set; }
        public double Fiyat { get; set; }
        public IFormFile? Resim { get; set; } // kullanıcının resim seçebilmesi için.
    }
}
