﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTickets.Promotion.DataAccess.Entities;
using GloboTickets.Promotion.Models;
using Microsoft.EntityFrameworkCore;

namespace GloboTickets.Promotion.DataAccess
{
    public class ActQueries
    {
        private readonly PromotionContext repository;

        public ActQueries(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task<List<ActModel>> ListActs()
        {
            var result = await repository.Act
                .Where(act => !act.Removed.Any())
                .Select(act => new
                {
                    ActGuid = act.ActGuid,
                    Description = act.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return result
                .Select(row => MapActModel(row.ActGuid, row.Description))
                .ToList();
        }

        public async Task<ActModel> GetAct(Guid actGuid)
        {
            var result = await repository.Act
                .Where(act => act.ActGuid == actGuid)
                .Select(act => new
                {
                    ActGuid = act.ActGuid,
                    Description = act.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .SingleOrDefaultAsync();

            return result == null ? null : MapActModel(result.ActGuid, result.Description);
        }

        private static ActModel MapActModel(Guid actGuid, ActDescription actDescription)
        {
            return new ActModel
            {
                ActGuid = actGuid,
                Title = actDescription?.Title,
                ImageHash = actDescription?.ImageHash,
                LastModifiedTicks = actDescription?.ModifiedDate.Ticks ?? 0
            };
        }
    }
}