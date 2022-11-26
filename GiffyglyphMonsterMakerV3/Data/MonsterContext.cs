﻿using GiffyglyphMonsterMakerV3.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class MonsterContext : DbContext
    {
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Feature> Features { get; set; }

        public MonsterContext(DbContextOptions<MonsterContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Monster>()
                .HasBaseType<Creature>();
            modelBuilder.Entity<Action>()
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
            //modelBuilder.Entity<Feature>().HasOne<Creature>(f => f.Parent).WithMany(c => c.Features)
             //   .HasForeignKey(f => f.Parent).OnDelete(DeleteBehavior.Cascade);
            /* modelBuilder.Entity<Creature>().OwnsMany<Feature>(c => c.Features, a =>
             {
                 a.WithOwner().HasForeignKey("ParentId");
                 a.Navigation(b => b.Parent).UsePropertyAccessMode(PropertyAccessMode.Property);
             });*/

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
        }
    }
}
