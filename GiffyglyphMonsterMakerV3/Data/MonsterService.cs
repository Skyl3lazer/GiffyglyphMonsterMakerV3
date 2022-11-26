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

        private readonly MonsterContext _context;
        #endregion

        public MonsterService(MonsterContext context)
        {
            _context = context;
        }

        public async Task<List<Monster>> GetAllMonstersAsync()
        {
            return await _context.Monsters.ToListAsync();
        }

        public async Task<bool> InsertMonsterAsync(Monster monster)
        {
            await _context.Monsters.AddAsync(monster);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMonsterAsync(Monster monster)
        {
            _context.Monsters.Update(monster);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMonster(Monster monster)
        {
            _context.Remove(monster);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}