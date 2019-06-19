using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AdministratorContext : DbContext
    {
        private readonly IConfiguration _config;
        public AdministratorContext (DbContextOptions<AdministratorContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config["ConnectionString:DefaultConnection"]);
        }
            
        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<User> Users { get; set; }
        public void Configuring()=> OnConfiguring(new DbContextOptionsBuilder());
    }
}
