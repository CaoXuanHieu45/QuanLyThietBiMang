using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Data;
using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Services;

/// <summary>Chứa các truy vấn đọc thiết bị.</summary>
public sealed class DeviceQueryService
{
    private readonly AppDbContext _context;

    public DeviceQueryService(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<DeviceResponse>> GetAllAsync() => _context.Devices
        .Select(device => new DeviceResponse
        {
            Id = device.Id,
            Name = device.Name,
            IpAddress = device.IpAddress,
            Status = device.Status,
            SiteId = device.SiteId,
            SiteName = device.Site != null ? device.Site.Name : string.Empty
        })
        .ToListAsync();

    public Task<DeviceResponse?> GetByIdAsync(int id) => _context.Devices
        .Where(device => device.Id == id)
        .Select(device => new DeviceResponse
        {
            Id = device.Id,
            Name = device.Name,
            IpAddress = device.IpAddress,
            Status = device.Status,
            SiteId = device.SiteId,
            SiteName = device.Site != null ? device.Site.Name : string.Empty
        })
        .FirstOrDefaultAsync();
}
