﻿using DigimonApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DigimonApp.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DigimonDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

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
                new Digimon { Id = 2, Name = "Greymon", Image = "https://digimon.shadowsmith.com/img/greymon.jpg", Level = "Champion" }
            );
        }
    }
}
