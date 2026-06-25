using Microsoft.EntityFrameworkCore;
using PA1.BL.Animals;
using System;
using System.IO;

namespace PA1.DAL
{
    public class AnimalContext : DbContext
    {
        public DbSet<Parrot> Parrots { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Animals.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parrot>(entity =>
            {
                entity.HasKey(p => p.ID);
                entity.Property(p => p.ID).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.FeatherColor).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Age).IsRequired();

                entity.HasOne(p => p.Trainer)
                .WithMany() 
                .HasForeignKey("TrainerId")
                .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.HasKey(t => t.Name);
                entity.Property(t => t.ExperienceYears).IsRequired();
            });

            // Seed a Trainer first
            var seedTrainer = new Trainer("Seed Trainer", 5);

            modelBuilder.Entity<Trainer>().HasData(seedTrainer);

            // Seed Parrots using anonymous objects (no read-only assignment error)
            modelBuilder.Entity<Parrot>().HasData(
                new { ID = "P001", Name = "Polly", Age = 3, FeatherColor = "Green", TrainerId = "Seed Trainer" },
                new { ID = "P002", Name = "Rio", Age = 2, FeatherColor = "Blue", TrainerId = "Seed Trainer" }
            );
        }
    }
}