using GiffyglyphMonsterMakerV3.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Z.EntityFramework.Plus;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Feature> Features { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Monster>()
                .HasBaseType<Creature>();

            modelBuilder.Entity<Action>()
                .HasBaseType<Feature>(); 
            modelBuilder.Entity<BonusAction>()
                .HasBaseType<Action>();
            modelBuilder.Entity<FreeAction>()
                .HasBaseType<Action>();
            modelBuilder.Entity<Reaction>()
                .HasBaseType<Action>();
            modelBuilder.Entity<Countermeasure>()
                .HasBaseType<Feature>();
            modelBuilder.Entity<Trait>()
                .HasBaseType<Feature>();
            

            modelBuilder.Entity<Creature>()
                .Property(e => e.Senses)
                .IsRequired()
                .HasConversion(
                    v => JsonSerializer.Serialize(v, typeof(Dictionary<SenseType, int>), JsonSerializerOptions.Default),
                    v => v == null
                        ? new Dictionary<SenseType, int>()
                        : JsonSerializer.Deserialize<Dictionary<SenseType, int>>(v, JsonSerializerOptions.Default)
                );
            modelBuilder.Entity<Creature>()
                .Property(e => e.OtherSpeeds)
                .IsRequired()
                .HasConversion(
                    v => JsonSerializer.Serialize(v, typeof(Dictionary<MovementType, int>), JsonSerializerOptions.Default),
                    v => v == null
                    ? new Dictionary<MovementType, int>()
                    : JsonSerializer.Deserialize<Dictionary<MovementType, int>>(v, JsonSerializerOptions.Default)
                    );
           modelBuilder.Entity<Creature>()
               .HasMany<Feature>(a=> a.Features)
               .WithOne()
               .HasForeignKey(a => a.ParentId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Creature>()
                .Property(e => e.Items)
                .HasConversion(
                    v=>string.Join('+',v),
                    v=>v.Split('+',StringSplitOptions.RemoveEmptyEntries).ToList()
                    );
            modelBuilder.Entity<Creature>()
                .Property(e => e.Languages)
                .HasConversion(
                    v => string.Join('+', v),
                    v => v.Split('+', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            modelBuilder.Entity<Creature>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(b => b.CreateUserId)
                .IsRequired();
            modelBuilder.Entity<Feature>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(b => b.CreateUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
