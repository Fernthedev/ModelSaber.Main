using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading;
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
        public DbSet<ModelUser> ModelUsers { get; set; }
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

                entity.HasMany(e => e.Users)
                    .WithOne(e => e.Model)
                    .HasForeignKey(e => e.ModelId);

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

            modelBuilder.Entity<ModelUser>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Model)
                    .WithMany(e => e.Users)
                    .HasForeignKey(e => e.ModelId);
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Models)
                    .HasForeignKey(e => e.UserId);
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

    public static class DbSetExtensions
    {
        // ReSharper disable PossibleInvalidOperationException
        public static Task<List<Model>> GetModelAsync(this DbSet<Model> models, int? first, DateTime? createdAfter, CancellationToken cancellationToken) => Task.FromResult(models.IncludeModelData().AsEnumerable().If(createdAfter.HasValue, x => x.Where(y => y.Date > createdAfter.Value)).If(first.HasValue, x => x.Take(first.Value)).ToList());
        public static Task<List<Model>> GetModelReverseAsync(this DbSet<Model> models, int? last, DateTime? createdBefore, CancellationToken cancellationToken) => Task.FromResult(models.IncludeModelData().AsEnumerable().If(createdBefore.HasValue, x => x.Where(y => y.Date < createdBefore.Value)).If(last.HasValue, x => x.TakeLast(last.Value)).ToList());
        public static Task<bool> GetModelNextPageAsync(this DbSet<Model> models, int? first, DateTime? createdAfter, CancellationToken cancellationToken) => Task.FromResult(models.If(createdAfter.HasValue, x => x.Where(y => y.Date > createdAfter.Value)).If(first.HasValue, x => x.Skip(first.Value)).Any());
        public static Task<bool> GetModelPreviousPageAsync(this DbSet<Model> models, int? last, DateTime? createdBefore, CancellationToken cancellationToken) => Task.FromResult(models.If(createdBefore.HasValue, x => x.Where(y => y.Date < createdBefore.Value)).If(last.HasValue, x => x.SkipLast(last.Value)).Any());
        public static IQueryable<Model> IncludeModelData(this IQueryable<Model> models) => models.Include(t => t.Tags).ThenInclude(t => t.Tag).Include(t => t.User).Include(t => t.Users).ThenInclude(t => t.User);
        public static Task<List<Tag>> GetTagAsync(this DbSet<Tag> tags, int? first, Guid? createdAfter, CancellationToken cancellationToken) => Task.FromResult(tags.IncludeTagData().AsEnumerable().If(createdAfter.HasValue, x => x.SkipWhile(y => y.CursorId != createdAfter.Value).Skip(1)).If(first.HasValue, x => x.Take(first.Value)).ToList());
        public static Task<List<Tag>> GetTagReverseAsync(this DbSet<Tag> tags, int? last, Guid? createdBefore, CancellationToken cancellationToken) => Task.FromResult(tags.IncludeTagData().AsEnumerable().If(createdBefore.HasValue, x => x.SkipWhile(y => y.CursorId != createdBefore.Value).Skip(1)).If(last.HasValue, x => x.TakeLast(last.Value)).ToList());
        public static Task<bool> GetTagNextPageAsync(this DbSet<Tag> tags, int? first, Guid? createdAfter, CancellationToken cancellationToken) => Task.FromResult(tags.If(createdAfter.HasValue, x => x.SkipWhile(y => y.CursorId != createdAfter.Value)).If(first.HasValue, x => x.Skip(first.Value)).Any());
        public static Task<bool> GetTagPreviousPageAsync(this DbSet<Tag> tags, int? last, Guid? createdBefore, CancellationToken cancellationToken) => Task.FromResult(tags.If(createdBefore.HasValue, x => x.TakeWhile(y => y.CursorId != createdBefore.Value)).If(last.HasValue, x => x.SkipLast(last.Value)).Any());
        public static IQueryable<Tag> IncludeTagData(this IQueryable<Tag> models) => models.Include(t => t.ModelTags).ThenInclude(t => t.Model).ThenInclude(t => t.Users).ThenInclude(t => t.User);
        // ReSharper restore PossibleInvalidOperationException
    }
}
