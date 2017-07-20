namespace SomethingWickedServer.Models
{
    public partial class Songs
    {
        public string Song { get; set; }
        public string Artist { get; set; }
        public int GenreId { get; set; }
        public string VideoGroup { get; set; }
        public string VideoId { get; set; }

        public virtual Genres Genre { get; set; }
    }
}
