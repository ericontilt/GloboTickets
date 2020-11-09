using System;
using System.Threading.Tasks;

namespace GloboTickets.Promotion
{
    public interface IShowCommands
    {
        Task ScheduleShow(Guid actGuid, Guid venueGuid, DateTimeOffset startTime);
        Task CancelShow(Guid actGuid, Guid venueGuid, DateTimeOffset startTime);
    }
}