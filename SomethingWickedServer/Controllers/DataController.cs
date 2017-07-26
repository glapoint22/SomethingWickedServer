using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using SomethingWickedServer.Models;

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

            //Video Groups
            List<MediaGroup> videoGroups = await db.VideoGroups.OrderByDescending(v => v.Date)
            .Select(v => new MediaGroup
            {
                id = v.Id,
                title = v.Title,
                thumbnail = v.Thumbnail
            })
            .AsNoTracking()
            .ToListAsync();

            //Photo Groups
            List<MediaGroup> photoGroups = await db.Photos.OrderByDescending(i => i.Id)
                .Select(p => new MediaGroup
                {
                    id = p.Id,
                    title = p.Title,
                    thumbnail = p.Thumbnail
                })
            .AsNoTracking()
            .ToListAsync();

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
                    time = x.DateTime.ToString("h tt") + " - " + x.DateTime.AddHours(x.Duration).ToString("h tt"),
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