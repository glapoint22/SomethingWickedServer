using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SomethingWickedServer.Controllers
{
    [Produces("application/json")]
    [Route("api/MediaGroups")]
    public class MediaGroupsController : Controller
    {
        public async Task<IActionResult> Get()
        {
            //Facebook
            string content = await Facebook.GetContent("me", "video_lists{title,thumbnail},albums{name,type,picture}");
            FacebookData facebookData = JsonConvert.DeserializeObject<FacebookData>(content);

            //Select the video groups and photo groups
            var mediaGroups = new
            {
                //Video Groups
                videoGroups = facebookData.video_lists.data
                .Select(v => new
                {
                    id = v.id,
                    title = v.title,
                    thumbnail = v.thumbnail
                })
                .ToList(),

                //Photo Groups
                photoGroups = facebookData.albums.data
                .Where(p => p.type == "normal")
                .Select(p => new
                {
                    id = p.id,
                    title = p.name,
                    thumbnail = p.picture.data.url
                })
                .ToList()
            };


            if (mediaGroups == null)
            {
                return NotFound();
            }

            return Ok(mediaGroups);
        }
    }
}