using GloboTickets.Promotion.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.DataAccess
{

    public class ShowQueries : IShowQueries
    {
        private PromotionContext repository;

        public ShowQueries(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task<List<ShowInfo>> ListShows(Guid actGuid)
        {
            var result = await repository.Show
                .Where(show =>
                    show.Act.ActGuid == actGuid &&
                    !show.Cancelled.Any())
                .ToListAsync();

            return result.Select(show => new ShowInfo
            {
                StartTime = show.StartTime
            })
                .ToList();
        }
    }
}