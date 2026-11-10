using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Data;
using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DevicesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _context.Devices
                .Include(d => d.Site)
                .Select(d => new
                {
                    d.Id,
                    d.Name,
                    d.IpAddress,
                    d.Status,
                    d.SiteId,
                    SiteName = d.Site != null ? d.Site.Name : string.Empty
                })
                .ToListAsync();

            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            var device = await _context.Devices
                .Include(d => d.Site)
                .Where(d => d.Id == id)
                .Select(d => new
                {
                    d.Id,
                    d.Name,
                    d.IpAddress,
                    d.Status,
                    d.SiteId,
                    SiteName = d.Site != null ? d.Site.Name : string.Empty
                })
                .FirstOrDefaultAsync();

            if (device == null) return NotFound();

            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevice([FromBody] Device deviceDto)
        {
            if (string.IsNullOrWhiteSpace(deviceDto.IpAddress) || !IPAddress.TryParse(deviceDto.IpAddress, out _))
            {
                return BadRequest(new { message = "Địa chỉ IP không hợp lệ." });
            }

            if (deviceDto.Status != "Online" && deviceDto.Status != "Offline")
            {
                return BadRequest(new { message = "Trạng thái chỉ được nhận giá trị 'Online' hoặc 'Offline'." });
            }

            var siteExists = await _context.Sites.AnyAsync(s => s.Id == deviceDto.SiteId);
            if (!siteExists)
            {
                return BadRequest(new { message = "SiteId không tồn tại." });
            }

            var device = new Device
            {
                Name = deviceDto.Name,
                IpAddress = deviceDto.IpAddress,
                Status = deviceDto.Status,
                SiteId = deviceDto.SiteId
            };

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] Device deviceDto)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();

            if (string.IsNullOrWhiteSpace(deviceDto.IpAddress) || !IPAddress.TryParse(deviceDto.IpAddress, out _))
            {
                return BadRequest(new { message = "Địa chỉ IP không hợp lệ." });
            }

            if (deviceDto.Status != "Online" && deviceDto.Status != "Offline")
            {
                return BadRequest(new { message = "Trạng thái chỉ được nhận giá trị 'Online' hoặc 'Offline'." });
            }

            var siteExists = await _context.Sites.AnyAsync(s => s.Id == deviceDto.SiteId);
            if (!siteExists)
            {
                return BadRequest(new { message = "SiteId không tồn tại." });
            }

            device.Name = deviceDto.Name;
            device.IpAddress = deviceDto.IpAddress;
            device.Status = deviceDto.Status;
            device.SiteId = deviceDto.SiteId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}