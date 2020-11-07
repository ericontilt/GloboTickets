﻿using System;
using System.Collections.Generic;

namespace GloboTickets.Promotion.DataAccess.Entities
{
    public class Show
    {
        public int ShowId { get; set; }

        public Act Act { get; set; }
        public int ActId { get; set; }
        public Venue Venue { get; set; }
        public int VenueId { get; set; }
        public DateTimeOffset StartTime { get; set; }

        public ICollection<ShowCancelled> Cancelled { get; set; } = new List<ShowCancelled>();
    }
}
