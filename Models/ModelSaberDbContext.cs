using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelSaber.Database.Models;

namespace ModelSaber.Database
{
    public class ModelSaberDbContext : DbContext
    {
        public DbSet<Model> Models { get; set; }
        public DbSet<ModelTag> ModelTags { get; set; }
        public DbSet<ModelVariation> ModelVariations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public ModelSaberDbContext(DbContextOptions<ModelSaberDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Database=modelsaber_v3;Username=postgres;Password=postgres").UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.ModelVariations)
                    .WithOne(e => e.ParentModel)
                    .HasForeignKey(e => e.ParentModelId);

                entity.HasMany(e => e.Tags)
                    .WithOne(e => e.Model)
                    .HasForeignKey(e => e.ModelId);

                entity.HasOne(e => e.ModelVariation)
                    .WithOne(e => e.Model)
                    .HasForeignKey<ModelVariation>(e => e.ModelId);

                entity.Ignore(e => e.Thumbnail);
                entity.Ignore(e => e.DownloadPath);
            });

            modelBuilder.Entity<ModelTag>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Model)
                    .WithMany(e => e.Tags)
                    .HasForeignKey(e => e.ModelId);

                entity.HasOne(e => e.Tag)
                    .WithMany(e => e.ModelTags)
                    .HasForeignKey(e => e.TagId);
            });

            modelBuilder.Entity<ModelVariation>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Model)
                    .WithOne(e => e.ModelVariation)
                    .HasForeignKey<ModelVariation>(e => e.ModelId);

                entity.HasOne(e => e.ParentModel)
                    .WithMany(e => e.ModelVariations)
                    .HasForeignKey(e => e.ParentModelId);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.ModelTags)
                    .WithOne(e => e.Tag)
                    .HasForeignKey(e => e.TagId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Models)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId);
            });
        }
    }
}
