using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AdministratorContext : DbContext
    {
        public AdministratorContext (DbContextOptions<AdministratorContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;port=5432;Database=Database;Username=postgres;Password=qwe123");
        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
