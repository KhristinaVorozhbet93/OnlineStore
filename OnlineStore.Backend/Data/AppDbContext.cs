using Microsoft.EntityFrameworkCore;
using OnlineStore.Backend.Data;

namespace OnlineStore.Backend
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>(); 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    }
}
