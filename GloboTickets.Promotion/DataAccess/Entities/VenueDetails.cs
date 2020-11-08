using System;
using System.ComponentModel.DataAnnotations;

namespace GloboTickets.Promotion.DataAccess.Entities
{
    public class VenueDetails
    {
        public int VenueDetailsId { get; set; }

        public Venue Venue { get; set; }
        public int VenueId { get; set; }
        public DateTime ModifiedDate { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string City { get; set; }
    }
}