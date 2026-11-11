using Microsoft.AspNetCore.Mvc;
using QuanLyThietBiMang.Repositories;

namespace QuanLyThietBiMang.Controllers
{
    [Route("api/sites")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly ISiteRepository _sites;

        public SitesController(ISiteRepository sites)
        {
            _sites = sites;
        }

        [HttpGet]
        public async Task<IActionResult> GetSites()
        {
            var sites = await _sites.GetAllAsync();

            return Ok(sites.Select(site => new { site.Id, site.Name }));
        }
    }
}
