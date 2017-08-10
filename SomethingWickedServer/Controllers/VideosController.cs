using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace SomethingWickedServer.Controllers
{
    [Produces("application/json")]
    [Route("api/videos/{id}")]
    public class VideosController : Controller
    {
        public async Task<IActionResult> Get(string id)
        {
            string content = await Facebook.GetContent(id, "title,videos{id}");
            JObject jObject = JObject.Parse(content);

            var facebookContent = new
            {
                title = (string)jObject.SelectToken("title"),
                data = jObject.SelectToken("videos.data")
                    .Select(a => new
                    {
                        id = (string)a["id"],
                        content = ""
                    }
                    )
                    .ToArray()
            };

            if (facebookContent == null)
            {
                return NotFound();
            }

            return Ok(facebookContent);

        }
    }
}