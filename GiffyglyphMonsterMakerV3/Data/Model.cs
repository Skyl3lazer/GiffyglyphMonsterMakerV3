using GiffyglyphMonsterMakerV3.Pages;
using Microsoft.EntityFrameworkCore;
using System;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class MonsterContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<MonsterAction> Actions { get; set; }
        public DbSet<MonsterBonusAction> BonusActions { get; set; }
        public string DbPath { get; }

        public MonsterContext(IConfiguration config)
        {
            _config = config;
            DbPath = _config["ConnectionStrings:ggmonstermaker"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer($@"Data Source={DbPath}");
    }
}
