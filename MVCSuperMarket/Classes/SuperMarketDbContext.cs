using Microsoft.EntityFrameworkCore;

namespace MVCSuperMarket.Classes
{
    public class SuperMarketDbContext : DbContext
    {
        public SuperMarketDbContext(DbContextOptions<SuperMarketDbContext> options) : base(options)
        {

        }
        public DbSet<Urun> Urunler => Set<Urun>();
    }
}
