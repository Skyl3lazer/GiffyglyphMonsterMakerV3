using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiffyglyphMonsterMakerV3.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using NuGet.Packaging;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class FolderService
    {
        #region Property

        private bool Loading = false;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IConfiguration _config;
        #endregion

        public FolderService(IDbContextFactory<ApplicationDbContext> dbContextFactory, AuthenticationStateProvider authenticationStateProvider, IConfiguration config)
        {
            _dbContextFactory = dbContextFactory;
            _authenticationStateProvider = authenticationStateProvider;
            _config = config;
        }


        public async Task<Folder> GetFolderByIdAsync(Guid id)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            var folder = _context.Folders.Single(a => a.Id == id);
            return folder;
        }
        public Folder GetFolderById(Guid id)
        {
            var task = Task.Run<Folder>(async () => await GetFolderByIdAsync(id));
            return task.Result;
        }
        public async Task<bool> InsertFolderAsync(Folder folder)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            try
            {
                Loading = true;
                await _context.AddAsync(folder);
                await _context.SaveChangesAsync();
            }
            finally
            {
                Loading = false;
            }
            return true;
        }

        public async Task<bool> UpdateFolderAsync(Folder folder)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            try
            {
                Loading = true;
                _context.Update(folder);
                await _context.SaveChangesAsync();
            }
            finally
            {
                Loading = false;
            }
            return true;
        }
        private void RemoveFolderContents(ApplicationDbContext _context, Folder folder)
        {
            _context.Remove(folder);

            foreach (Creature creature in folder.Creatures)
            {
                _context.Remove(creature);
            }

            foreach (var child in folder.Children)
            {
                RemoveFolderContents(_context, child);
            }
        }
        public async Task<bool> DeleteFolderAsync(Folder folder, bool overrideDelete = false, bool cascade = false)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            if (!overrideDelete && folder.HasChildMonsters())
            {
                return false;
            }

            try
            {
                Loading = true;
                _context.Remove(folder);
                if (cascade)
                {
                    foreach (Creature creature in folder.Creatures)
                    {
                        _context.Remove(creature);
                    }
                    foreach (var child in folder.Children)
                    {
                        RemoveFolderContents(_context, child);
                    }
                }
                else
                {
                    foreach(var child in folder.Children)
                    {
                        folder.Parent.Children.Add(child);
                        child.ParentId = folder.ParentId;
                        child.Parent = folder.Parent;
                        _context.Update(child);
                    }
                    foreach (var creature in folder.Creatures)
                    {
                        folder.Parent.Creatures.Add(creature);
                        creature.FolderId = folder.ParentId;
                        creature.Folder = folder.Parent;
                        _context.Update(creature);
                    }

                }

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