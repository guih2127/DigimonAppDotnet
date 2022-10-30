using DigimonApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DigimonApp.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Digimon> Digimons { get; set; }

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
                new Digimon { Id = 1, Name = "Agumon", Image = "https://digimon.shadowsmith.com/img/agumon.jpg", Level = "Rookie" },
                new Digimon { Id = 2, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 3, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 4, Name = "Agumon", Image = "https://digimon.shadowsmith.com/img/agumon.jpg", Level = "Rookie" },
                new Digimon { Id = 5, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 6, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 7, Name = "Agumon", Image = "https://digimon.shadowsmith.com/img/agumon.jpg", Level = "Rookie" },
                new Digimon { Id = 8, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 9, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 10, Name = "Agumon", Image = "https://digimon.shadowsmith.com/img/agumon.jpg", Level = "Rookie" },
                new Digimon { Id = 11, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 12, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 13, Name = "Agumon", Image = "https://digimon.shadowsmith.com/img/agumon.jpg", Level = "Rookie" },
                new Digimon { Id = 14, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 15, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 16, Name = "Agumon", Image = "https://digimon.shadowsmith.com/img/agumon.jpg", Level = "Rookie" },
                new Digimon { Id = 17, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 18, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 19, Name = "Agumon", Image = "https://digimon.shadowsmith.com/img/agumon.jpg", Level = "Rookie" },
                new Digimon { Id = 20, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 21, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 22, Name = "Agumon", Image = "https://digimon.shadowsmith.com/img/agumon.jpg", Level = "Rookie" },
                new Digimon { Id = 23, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" },
                new Digimon { Id = 24, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" }
            );
        }
    }
}
