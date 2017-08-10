using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SomethingWickedServer.Models;

namespace SomethingWickedServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Bios")]
    public class BiosController : Controller
    {
        private readonly Something_WickedContext _context;

        public BiosController(Something_WickedContext context)
        {
            _context = context;
        }

        
        // GET: api/Bios/Amy
        [HttpGet("{name}")]
        public async Task<IActionResult> GetBios([FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bio = await _context.Bios
                .Include(m => m.Member)
                .Where(b => b.Member.Name == name)
                    .Select(a => new {
                    title = a.Member.Name,
                    a.Bio,
                    a.Member.Thumbnail
                })
                .SingleOrDefaultAsync();

            if (bio == null)
            {
                return NotFound();
            }

            return Ok(bio);
        }
    }
}