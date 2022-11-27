﻿using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiffyglyphMonsterMakerV3.Utility;
using Microsoft.EntityFrameworkCore;

namespace GiffyglyphMonsterMakerV3.Data
{
    public class FeatureService
    {
        #region Property

        private bool Loading = false;
        private readonly IDbContextFactory<MonsterContext> _dbContextFactory;
        #endregion

        public FeatureService(IDbContextFactory<MonsterContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Feature>> GetAllFeatureTemplatesAsync()
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync();
            //new Guid() intentionally used here to get the all-0 GUID used for feature templates
            var ret = await _context.Features.Where(a => a.ParentId == new Guid()).ToListAsync();
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
                //Intentional new Guid() use for 0's
                featureClone.ParentId = new Guid();
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