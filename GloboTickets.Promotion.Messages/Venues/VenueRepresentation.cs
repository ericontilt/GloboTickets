using System;

namespace GloboTickets.Promotion.Messages.Venues
{
    public class VenueRepresentation
    {
        public Guid venueGuid { get; set; }
        public VenueDescriptionRepresentation description { get; set; }
        public VenueLocationRepresentation location { get; set; }
    }
}