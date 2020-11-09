using System;

namespace GloboTickets.Promotion.Info
{
    public class ActInfo
    {
        public Guid ActGuid { get; set; }
        public string Title { get; set; }
        public string ImageHash { get; set; }
        public long LastModifiedTicks { get; set; }
    }
}