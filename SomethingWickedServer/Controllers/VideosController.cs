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
        public async Task<Content> Get(string id)
        {
            string content = await Facebook.GetContent(id, "title,videos{id}");
            JObject jObject = JObject.Parse(content);

            return new Content
            {
                title = (string)jObject.SelectToken("title"),
                data = jObject.SelectToken("videos.data")
                    .Select(a => new ContentData
                    {
                        id = (string)a["id"],
                        url = null
                    }
                    )
                    .ToArray()
            };

        }
    }
}