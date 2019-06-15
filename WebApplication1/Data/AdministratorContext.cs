using Microsoft.EntityFrameworkCore;

namespace AdministratorPanelMvc.Data
{
    public class AdministratorContext : DbContext
    {
        public AdministratorContext (DbContextOptions<AdministratorContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;port=5432;Database=Database;Username=postgres;Password=qwe123");
        public DbSet<AdministratorPanelMvc.Models.Announcement> Announcement { get; set; }
        public DbSet<AdministratorPanelMvc.Models.User> Users { get; set; }
    }
}
