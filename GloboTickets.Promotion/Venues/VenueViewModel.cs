using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GloboTickets.Promotion.Venues
{
    public class VenueViewModel
    {
        public VenueInfo Venue { get; set; }
        public List<SelectListItem> TimeZones { get; set; }
    }
}