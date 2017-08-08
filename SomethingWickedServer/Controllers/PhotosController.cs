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
        public async Task<Content> Get(string id)
        {
            string content = await Facebook.GetContent(id, "name,photos{images}");
            JObject jObject = JObject.Parse(content);

            return new Content
            {
                title = (string)jObject.SelectToken("name"),
                data = jObject.SelectToken("photos.data")
                    .Select(a => new ContentData
                        {
                            id = (string)a["id"],
                            url = a["images"]
                            .Select(b => (string)b["source"])
                            .FirstOrDefault()
                        }
                    )
                    .ToArray()
            };

        }
    }
}