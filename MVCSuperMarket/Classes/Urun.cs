namespace MVCSuperMarket.Classes
{
    public class Urun
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public bool StoktaVarMi { get; set; }
        public double Fiyat { get; set; }
        public string? Resim { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
