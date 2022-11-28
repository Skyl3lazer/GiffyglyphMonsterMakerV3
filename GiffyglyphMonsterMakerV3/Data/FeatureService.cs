﻿using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiffyglyphMonsterMakerV3.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class FeatureService
    {
        #region Property

        private bool Loading = false;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        #endregion

        public FeatureService(IDbContextFactory<ApplicationDbContext> dbContextFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Feature>> GetAllFeatureTemplatesAsync()
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();
            var ret = await _context.Features.Where(a => a.ParentId == null)
                .Include(f=>f.Frequency)
                .ToListAsync();
            return ret;
        }

        public async Task<Feature> GetFeatureByIdAsync(Guid id)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            var feature = _context.Features.Single(a => a.Id == id);
            return feature;
        }
        public Feature GetFeatureById(Guid id)
        {
            var task = Task.Run<Feature>(async () => await GetFeatureByIdAsync(id));
            return task.Result;
        }
        public async Task<bool> InsertFeatureAndTemplateAsync(Feature feature)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            try
            {
                Loading = true;
                var featureClone = feature.Clone();
                featureClone.ParentId = null;
                featureClone.Id = Guid.NewGuid();
                feature.TemplateId = featureClone.Id;
                await _context.AddAsync(feature);
                await _context.AddAsync(featureClone);
                await _context.SaveChangesAsync();
            }
            finally
            {
                Loading = false;
            }
            return true;
        }
        public async Task<bool> InsertFeatureAsync(Feature feature)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            try
            {
                Loading = true;
                await _context.AddAsync(feature);
                await _context.SaveChangesAsync();
            }
            finally
            {
                Loading = false;
            }
            return true;
        }

        public async Task<bool> UpdateFeatureAsync(Feature feature)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            try
            {
                Loading = true;
                _context.Update(feature);
                await _context.SaveChangesAsync();
            }
            finally
            {
                Loading = false;
            }
            return true;
        }

        public async Task<bool> DeleteFeatureAsync(Feature feature)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();

            if (Loading)
            {
                return false;
            }

            try
            {
                Loading = true;
                _context.Remove(feature);
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