using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelSaber.Models;

namespace ModelSaber.Database
{
    public class ModelSaberDbContext : DbContext
    {
        public DbSet<Model> Models { get; set; } = null!;
        public DbSet<ModelTag> ModelTags { get; set; } = null!;
        public DbSet<ModelVariation> ModelVariations { get; set; } = null!;
        public DbSet<ModelUser> ModelUsers { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Vote> Votes { get; set; } = null!;
        public DbSet<UserLogons> Logons { get; set; } = null!;
        public DbSet<UserTags> UserTags { get; set; } = null!;

        public ModelSaberDbContext(DbContextOptions<ModelSaberDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(getConString(), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)).UseSnakeCaseNamingConvention();

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

                entity.HasMany(e => e.Votes)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Logon)
                    .WithOne(e => e.User!)
                    .HasForeignKey<UserLogons>(e => e.UserId);

                entity.HasMany(e => e.UserTags)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Model)
                    .WithMany(e => e.Votes)
                    .HasForeignKey(e => e.ModelId);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Votes)
                    .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<UserLogons>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.User)
                    .WithOne(e => e!.Logon)
                    .HasForeignKey<UserLogons>(e => e.UserId);
            });

            modelBuilder.Entity<UserTags>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.UserTags)
                    .HasForeignKey(e => e.UserId);
            });
        }

        private string getConString()
        {
            var db = "modelsaber_v3";

            var envPW = Environment.GetEnvironmentVariable("MODELSABER_DB_PW");
            var envUser = Environment.GetEnvironmentVariable("MODELSABER_DB_USER");
            var envHost = Environment.GetEnvironmentVariable("MODELSABER_DB_HOST");
            
            var password = envPW.TryGetValue("postgres");
            var user = envUser.TryGetValue("postgres");
            var host = envHost.TryGetValue("localhost");

            return $"Host={host};Database={db};Username={user};Password={password}";
        }
    }

    public static class DbSetExtensions
    {
        // ReSharper disable PossibleInvalidOperationException
        public static Task<List<Model>> GetModelAsync(this DbSet<Model> models, int? first, Guid? createdAfter, TypeEnum? mType, CancellationToken cancellationToken) => 
            Task.FromResult(models.IncludeModelData().OrderBy(t => t.Id).ToList()
                .If(mType.HasValue, x => x.Where(t => t.Type == mType!.Value))
                .If(createdAfter.HasValue, x => x.SkipWhile(y => y.Uuid != createdAfter!.Value).Skip(1))
                .If(first.HasValue, x => x.Take(first!.Value)).ToList());

        public static Task<List<Model>> GetModelReverseAsync(this DbSet<Model> models, int? last, Guid? createdBefore, TypeEnum? mType, CancellationToken cancellationToken) => 
            Task.FromResult(models.IncludeModelData().OrderByDescending(t => t.Id).ToList()
                .If(mType.HasValue, x => x.Where(t => t.Type == mType!.Value))
                .If(createdBefore.HasValue, x => x.SkipWhile(y => y.Uuid != createdBefore!.Value).Skip(1))
                .If(last.HasValue, x => x.Take(last!.Value)).ToList());

        public static Task<bool> GetModelNextPageAsync(this DbSet<Model> models, CancellationToken cancellationToken, int? id) => id.HasValue ?
            Task.FromResult(models.Any(t => t.Id > id.Value))
            : Task.FromResult(false);

        public static Task<bool> GetModelPreviousPageAsync(this DbSet<Model> models, CancellationToken cancellationToken, int? id) => id.HasValue ?
            Task.FromResult(models.Any(t => t.Id < id.Value))
            : Task.FromResult(false);

        public static IQueryable<Model> IncludeModelData(this IQueryable<Model> models) => 
            models.Include(t => t.Tags).ThenInclude(t => t.Tag).Include(t => t.User).Include(t => t.Users).ThenInclude(t => t.User).ThenInclude(t => t.UserTags);

        public static Task<List<Tag>> GetTagAsync(this DbSet<Tag> tags, int? first, Guid? createdAfter, CancellationToken cancellationToken) => 
            Task.FromResult(tags.IncludeTagData().ToList()
                .If(createdAfter.HasValue, x => x.SkipWhile(y => y.CursorId != createdAfter!.Value).Skip(1))
                .If(first.HasValue, x => x.Take(first!.Value)).ToList());

        public static Task<List<Tag>> GetTagReverseAsync(this DbSet<Tag> tags, int? last, Guid? createdBefore, CancellationToken cancellationToken) => 
            Task.FromResult(tags.IncludeTagData().OrderByDescending(t => t.Id).ToList()
                .If(createdBefore.HasValue, x => x.SkipWhile(y => y.CursorId != createdBefore!.Value).Skip(1))
                .If(last.HasValue, x => x.Take(last!.Value)).ToList());

        public static Task<bool> GetTagNextPageAsync(this DbSet<Tag> tags, CancellationToken cancellationToken, int? id) => id.HasValue ?
            Task.FromResult(tags.Any(t => t.Id < id.Value))
            : Task.FromResult(false);

        public static Task<bool> GetTagPreviousPageAsync(this DbSet<Tag> tags, CancellationToken cancellationToken, int? id) => id.HasValue ?
            Task.FromResult(tags.Any(t => t.Id < id.Value))
            : Task.FromResult(false);

        public static IQueryable<Tag> IncludeTagData(this IQueryable<Tag> models) => 
            models.Include(t => t.ModelTags).ThenInclude(t => t.Model).ThenInclude(t => t.Users).ThenInclude(t => t.User).ThenInclude(t => t.UserTags);
        // ReSharper restore PossibleInvalidOperationException
        public static string TryGetValue(this string? s, string def)
        {
            return s ?? def;
        }
    }
}
