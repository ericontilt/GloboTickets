using System;

namespace GloboTickets.Promotion.DataAccess.Entities
{
    public class ShowCancelled
    {
        public int ShowCancelledId { get; set; }

        public Show Show { get; set; }
        public int ShowId { get; set; }
        public DateTime CancelledDate { get; set; }
    }
}
