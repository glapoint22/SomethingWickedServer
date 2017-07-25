using System.Collections.Generic;

namespace SomethingWickedServer
{
    public class Data
    {
        public Data(List<ShowcaseImage> showcaseImages, List<Show> shows, List<Song> songs, List<MediaGroup> videoGroups, List<MediaGroup> photoGroups, List<Member> members)
        {
            this.showcaseImages = showcaseImages;
            this.shows = shows;
            this.songs = songs;
            this.videoGroups = videoGroups;
            this.photoGroups = photoGroups;
            this.members = members;
        }

        public List<ShowcaseImage> showcaseImages { get; set; }
        public List<Show> shows { get; set; }
        public List<Song> songs { get; set; }
        public List<MediaGroup> videoGroups { get; set; }
        public List<MediaGroup> photoGroups { get; set; }
        public List<Member> members { get; set; }
    }
}
