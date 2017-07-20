using System.Collections.Generic;

namespace SomethingWickedServer.Models
{
    public partial class Venues
    {
        public Venues()
        {
            Schedule = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}
