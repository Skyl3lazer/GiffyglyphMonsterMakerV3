using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class MonsterService
    {
        #region Property

        private bool Loading = false;
        private readonly IDbContextFactory<MonsterContext> _dbContextFactory;
        #endregion

        public MonsterService(IDbContextFactory<MonsterContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Monster>> GetAllMonstersAsync()
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();
            
            await _context.Monsters
                .Include(a => a.Features)
                    .ThenInclude(f => f.Frequency)
                .Include(m => m.Offense)
                .Include(m => m.Defenses)
                    .ThenInclude(d=>d.ProficientSavingThrows)
                .Include(m => m.Attributes)
                .LoadAsync();
            return await _context.Monsters.ToListAsync();
        }

        public Monster GetTemplateMonster()
        {
            //Intentional use of new Guid for 0's
            return new Monster() { Id = new Guid() };
        }
        public async Task<Monster> GetMonsterByIdAsync(Guid id)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            var monster = _context.Monsters.Single(a => a.Id == id);
            return monster;
        }
        public Monster GetMonsterById(Guid id)
        {
            var task = Task.Run<Monster>(async () => await GetMonsterByIdAsync(id));
            return task.Result;
        }
        public async Task<bool> InsertMonsterAsync(Monster monster)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            try
            {
                Loading = true;
                await _context.AddAsync(monster);
                await _context.SaveChangesAsync();
            }
            finally
            {
                Loading = false;
            }
            return true;
        }

        public async Task<bool> UpdateMonsterAsync(Monster monster)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            try
            {
                _context.Update(monster);
                await _context.SaveChangesAsync();
            }
            finally
            {
                Loading = false;
            }
            return true;
        }

        public async Task<bool> DeleteMonsterAsync(Monster monster)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            try
            {
                Loading = true;
                _context.Remove(monster);
                await _context.SaveChangesAsync();
            }
            finally
            {
                Loading = false;
            }
            return true;
        }
    }
}