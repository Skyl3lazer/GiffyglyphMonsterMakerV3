using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Z.EntityFramework.Plus;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class MonsterService
    {
        #region Property

        private bool Loading = false;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        #endregion

        public MonsterService(IDbContextFactory<ApplicationDbContext> dbContextFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            _dbContextFactory = dbContextFactory;
            _authenticationStateProvider = authenticationStateProvider;
            
        }

        public async Task<List<Monster>> GetAllMonstersAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var User = authState.User;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            _context.Filter<Monster>(q => q.Where(m => m.CreateUserId == currentUserId));

            await _context.Monsters
                 .Include(a => a.Features)
                     .ThenInclude(f => f.Frequency)
                 .Include(m => m.Offense)
                 .Include(m => m.Defenses)
                     .ThenInclude(d=>d.ProficientSavingThrows)
                 .Include(m => m.Attributes)
                 .LoadAsync();

            /*
             await _context.Monsters
                 .Include(a => a.Features)
                 .ThenInclude(f => f.Frequency)
                 .Include(m => m.Offense)
                 .Include(m => m.Defenses)
                 .ThenInclude(d => d.ProficientSavingThrows)
                 .Include(m => m.Attributes)
                 .LoadAsync();
            */
            return await _context.Monsters.ToListAsync();
        }
        public async Task<Monster> GetMonsterByIdAsync(Guid id)
        {
            using var _context = _dbContextFactory.CreateDbContext();

            var mon = _context.Monsters.Find(id);
            return mon;
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