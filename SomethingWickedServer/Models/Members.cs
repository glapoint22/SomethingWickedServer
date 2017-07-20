using System.Collections.Generic;

namespace SomethingWickedServer.Models
{
    public partial class Members
    {
        public Members()
        {
            Bios = new HashSet<Bios>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }

        public virtual ICollection<Bios> Bios { get; set; }
    }
}
