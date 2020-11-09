using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GloboTickets.Promotion.Info;

namespace GloboTickets.Promotion.DataAccess
{
    public interface IActQueries
    {
        Task<List<ActInfo>> ListActs();
        Task<ActInfo> GetAct(Guid actGuid);
    }
}