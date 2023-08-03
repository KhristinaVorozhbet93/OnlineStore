using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Backend
{
    public class AppDbContext : DbContext
    {
        DbSet<Product> Products => Set<Product>(); 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    }
}
