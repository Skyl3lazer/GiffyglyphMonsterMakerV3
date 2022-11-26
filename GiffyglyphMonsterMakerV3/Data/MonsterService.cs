using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class MonsterService
    {
        #region Property

        private bool Loading = false;
        private readonly MonsterContext _context;
        #endregion

        public MonsterService(MonsterContext context)
        {
            _context = context;
        }

        public async Task<List<Monster>> GetAllMonstersAsync()
        {
            await _context.Monsters.Include(a => a.Features).LoadAsync();
            await _context.Features.Include(a=>a.Parent).LoadAsync();
            return await _context.Monsters.ToListAsync();
        }

        public async Task<Monster> GetMonsterByIdAsync(Guid id)
        {
            var monster = _context.Monsters.Single(a => a.Id == id);
            await _context.Entry(monster).Collection(b => b.Features).LoadAsync();
            return monster;
        }
        public Monster GetMonsterById(Guid id)
        {
            var task = Task.Run<Monster>(async () => await GetMonsterByIdAsync(id));
            return task.Result;
        }
        public async Task<bool> InsertMonsterAsync(Monster monster)
        {
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
            if (Loading)
            {
                return false;
            }

            try
            {
                Loading = true;
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