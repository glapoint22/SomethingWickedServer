using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace SomethingWickedServer.Controllers
{
    [Produces("application/json")]
    [Route("api/photos/{id}")]
    public class PhotosController : Controller
    {
        public async Task<IActionResult> Get(string id)
        {
            string content = await Facebook.GetContent(id, "name,photos{images}");
            JObject jObject = JObject.Parse(content);

            var facebookContent = new 
            {
                title = (string)jObject.SelectToken("name"),
                data = jObject.SelectToken("photos.data")
                    .Select(a => new
                    {
                        id = (string)a["id"],
                        content = a["images"]
                            .Select(b => (string)b["source"])
                            .FirstOrDefault()
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