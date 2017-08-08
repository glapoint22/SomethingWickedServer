using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using SomethingWickedServer.Models;
using Newtonsoft.Json;

namespace SomethingWickedServer
{
    [Produces("application/json")]
    [Route("api/Data")]
    public class DataController : Controller
    {
        private Something_WickedContext db;
        public DataController(Something_WickedContext dbContext)
        {
            db = dbContext;
        }

        public async Task<Data> Get()
        {
            //Showcase Images
            List<ShowcaseImage> showcaseImages = await db.ShowcaseImages
                .Select(m => new ShowcaseImage
                {
                    name = m.Name,
                })
                .AsNoTracking()
                .ToListAsync();


            //Songs
            List<Song> songs = await db.Songs.Include(s => s.Genre)
                .OrderBy(s => s.Song)
                .Select(s => new Song
                {
                    name = s.Song,
                    genre = s.Genre.Genre,
                    artist = s.Artist,
                    videoGroup = s.VideoGroup,
                    videoID = s.VideoId == null ? "z" : s.VideoId
                })
                .AsNoTracking()
                .ToListAsync();

            //Facebook
            string content = await Facebook.GetContent("me", "video_lists{title,thumbnail},albums{name,type,picture}");
            FacebookData facebookData = JsonConvert.DeserializeObject<FacebookData>(content);

            //Video Groups
            List<MediaGroup> videoGroups = facebookData.video_lists.data
                .Select(v => new MediaGroup
                {
                    id = v.id,
                    title = v.title,
                    thumbnail = v.thumbnail
                })
                .ToList();

            //Photo Groups
            List<MediaGroup> photoGroups = facebookData.albums.data
                .Where(p => p.type == "normal")
                .Select(p => new MediaGroup
                {
                    id = p.id,
                    title = p.name,
                    thumbnail = p.picture.data.url
                })
                .ToList();


            //Members
            List<Member> members = await db.Members
                .Select(m => new Member
                {
                    name = m.Name,
                    thumbnail = m.Thumbnail
                })
            .AsNoTracking()
            .ToListAsync();

            //Shows
            List<Show> shows = await db.Schedule.Include(s => s.Venue)
                .Where(x => x.DateTime.Date > DateTime.Now.Date)
                .Select(x => new Show
                {
                    date = x.DateTime.ToString("MMMM dd"),
                    time = x.DateTime.ToString("h:mm tt") + " - " + x.DateTime.AddHours(x.Duration).ToString("h:mm tt"),
                    venue = new Venue
                    {
                        name = x.Venue.Name,
                        location = x.Venue.Location,
                        url = x.Venue.Url
                    }
                })
                .AsNoTracking()
                .ToListAsync();
            

            return new Data(showcaseImages, shows, songs, videoGroups, photoGroups, members);
        }
    }
}