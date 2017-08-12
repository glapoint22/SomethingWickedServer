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

        public async Task<IActionResult> Get()
        {
            var data = new
            {
                //Showcase Images
                showcaseImages = await db.ShowcaseImages
                    .Select(m => new
                    {
                        name = m.Name,
                    })
                    .AsNoTracking()
                    .ToListAsync(),

                //Shows
                shows = await db.Schedule.Include(s => s.Venue)
                    .Where(x => x.DateTime.Date > DateTime.Now.Date)
                    .Select(x => new
                    {
                        date = x.DateTime.ToString("MMMM dd"),
                        time = x.DateTime.ToString("h:mm tt") + " - " + x.DateTime.AddHours(x.Duration).ToString("h:mm tt"),
                        venue = new
                        {
                            name = x.Venue.Name,
                            location = x.Venue.Location,
                            url = x.Venue.Url
                        }
                    })
                    .AsNoTracking()
                    .ToListAsync(),

                //Songs
                songs = await db.Songs.Include(s => s.Genre)
                    .OrderBy(s => s.Song)
                    .Select(s => new
                    {
                        name = s.Song,
                        genre = s.Genre.Genre,
                        artist = s.Artist,
                        videoGroup = s.VideoGroup,
                        videoID = s.VideoId == null ? "z" : s.VideoId
                    })
                    .AsNoTracking()
                    .ToListAsync(),

                //Members
                members = await db.Members
                    .Select(m => new
                    {
                        name = m.Name,
                        thumbnail = m.Thumbnail
                    })
                    .AsNoTracking()
                    .ToListAsync()
            };

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}