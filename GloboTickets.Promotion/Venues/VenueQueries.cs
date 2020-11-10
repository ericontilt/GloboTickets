using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTickets.Promotion.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GloboTickets.Promotion.Venues
{
    public class VenueQueries
    {
        private readonly PromotionContext repository;

        public VenueQueries(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task<List<VenueInfo>> ListVenues()
        {
            var result = await repository.Venue
                .Select(venue => new
                {
                    venue.VenueGuid,
                    Description = venue.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return result
                .Select(row => MapVenue(row.VenueGuid, row.Description))
                .ToList();
        }

        public async Task<VenueInfo> GetVenue(Guid venueGuid)
        {
            var result = await repository.Venue
                .Where(venue => venue.VenueGuid == venueGuid)
                .Select(venue => new
                {
                    venue.VenueGuid,
                    Description = venue.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .SingleOrDefaultAsync();

            return result == null ? null : MapVenue(result.VenueGuid, result.Description);
        }

        private VenueInfo MapVenue(Guid venueGuid, VenueDescription venueDescription)
        {
            return new VenueInfo
            {
                VenueGuid = venueGuid,
                Name = venueDescription?.Name,
                City = venueDescription?.City,
                LastModifiedTicks = venueDescription?.ModifiedDate.Ticks ?? 0
            };
        }
    }
}