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

        public List<ShowcaseImage> showcaseImages;
        public List<Show> shows;
        public List<Song> songs;
        public List<MediaGroup> videoGroups;
        public List<MediaGroup> photoGroups;
        public List<Member> members;
    }
}
