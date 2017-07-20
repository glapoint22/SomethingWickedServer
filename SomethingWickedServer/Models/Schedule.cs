using System;

namespace SomethingWickedServer.Models
{
    public partial class Schedule
    {
        public DateTime DateTime { get; set; }
        public double Duration { get; set; }
        public int VenueId { get; set; }

        public virtual Venues Venue { get; set; }
    }
}
