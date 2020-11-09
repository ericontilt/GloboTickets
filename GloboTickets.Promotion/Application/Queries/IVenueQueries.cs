using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GloboTickets.Promotion.Info;

namespace GloboTickets.Promotion.DataAccess
{
    public interface IVenueQueries
    {
        Task<List<VenueInfo>> ListVenues();
        Task<VenueInfo> GetVenue(Guid venueGuid);
    }
}