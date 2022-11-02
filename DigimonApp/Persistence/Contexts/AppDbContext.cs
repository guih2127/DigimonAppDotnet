using DigimonApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DigimonApp.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Digimon> Digimons { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Digimon>().ToTable("Digimons");
            builder.Entity<Digimon>().HasKey(p => p.Id);
            builder.Entity<Digimon>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Digimon>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Digimon>().Property(p => p.Level).IsRequired().HasMaxLength(30);

            builder.Entity<Digimon>().HasData
            (
                new Digimon { Id = 1, Name = "Agumon", Image = "https://digimon.shadowsmith.com/img/agumon.jpg", Level = DigimonLevelEnum.ROOKIE }
            );
        }
    }
}
