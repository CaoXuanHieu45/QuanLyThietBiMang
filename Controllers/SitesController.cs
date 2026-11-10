using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Data;

namespace QuanLyThietBiMang.Controllers
{
    [Route("api/sites")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SitesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSites()
        {
            var sites = await _context.Sites
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();

            return Ok(sites);
        }
    }
}