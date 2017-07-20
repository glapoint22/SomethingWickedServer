using System.Collections.Generic;

namespace SomethingWickedServer.Models
{
    public partial class Genres
    {
        public Genres()
        {
            Songs = new HashSet<Songs>();
        }

        public int Id { get; set; }
        public string Genre { get; set; }

        public virtual ICollection<Songs> Songs { get; set; }
    }
}
