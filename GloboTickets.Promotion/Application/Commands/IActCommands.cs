using GloboTickets.Promotion.Info;
using System;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.DataAccess
{
    public interface IActCommands
    {
        Task SaveAct(ActInfo actModel);
        Task RemoveAct(Guid actGuid);
    }
}