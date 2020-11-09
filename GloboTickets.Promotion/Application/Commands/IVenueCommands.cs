using GloboTickets.Promotion.Info;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.DataAccess
{
    public interface IVenueCommands
    {
        Task SaveVenue(VenueInfo venueModel);
    }
}