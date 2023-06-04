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

namespace GiffyglyphMonsterMakerV3.Data
{
    public class FolderService
    {/*
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


        public async Task<Folder> GetFolderByIdAsync(string id)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            var folder = _context.Folders.Single(a => a.Id == id);
            return folder;
        }
        public Folder GetFolderById(string id)
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

        public async Task<bool> UpdateFeatureAsync(Folder folder)
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

        public async Task<bool> DeleteFeatureAsync(Folder folder, bool cascade = false)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            if(_context.Folders.Any(a => a.HierarchyId.GetAncestor(folder.Id)) { }

            try
            {
                Loading = true;
                _context.Remove(folder);
                if (cascade)
                {
                    foreach (var item in _context.Folders.Where(a => a.Id == folder.Id))
                    {
                        item.TemplateId = null;
                    }
                }

                await _context.SaveChangesAsync();
            }
            finally
            {
                Loading = false;
            }
            return true;
        }*/
    }
}