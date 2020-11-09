using System;
using System.Collections.Generic;

namespace GloboTickets.Promotion.DataAccess.Entities
{
    public class Act
    {
        public int ActId { get; set; }
        public Guid ActGuid { get; set; }

        public ICollection<ActDescription> Descriptions { get; set; } = new List<ActDescription>();
        public ICollection<ActRemoved> Removed { get; set; } = new List<ActRemoved>();
    }
}