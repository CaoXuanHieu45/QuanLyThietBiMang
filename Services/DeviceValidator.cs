using System.Net;
using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Data;
using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Services;

/// <summary>Kiểm tra các quy tắc dữ liệu của thiết bị.</summary>
public sealed class DeviceValidator
{
    private readonly AppDbContext _context;

    public DeviceValidator(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string?> ValidateAsync(Device device)
    {
        if (string.IsNullOrWhiteSpace(device.IpAddress) || !IPAddress.TryParse(device.IpAddress, out _))
        {
            return "Địa chỉ IP không hợp lệ.";
        }

        if (device.Status is not ("Online" or "Offline"))
        {
            return "Trạng thái chỉ được nhận giá trị 'Online' hoặc 'Offline'.";
        }

        var siteExists = await _context.Sites.AnyAsync(site => site.Id == device.SiteId);
        return siteExists ? null : "SiteId không tồn tại.";
    }
}
