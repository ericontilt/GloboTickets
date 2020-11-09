using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.DataAccess
{
    public interface IShowQueries
    {
        Task<List<ShowInfo>> ListShows(Guid actGuid);
    }
}