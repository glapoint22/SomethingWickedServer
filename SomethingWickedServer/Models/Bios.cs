namespace SomethingWickedServer.Models
{
    public partial class Bios
    {
        public int BioId { get; set; }
        public string Bio { get; set; }
        public int MemberId { get; set; }

        public virtual Members Member { get; set; }
    }
}
