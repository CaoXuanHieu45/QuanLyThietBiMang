using System.Net;
using QuanLyThietBiMang.Models;
using QuanLyThietBiMang.Repositories;

namespace QuanLyThietBiMang.Services;

/// <summary>Kiểm tra các quy tắc dữ liệu của thiết bị.</summary>
public sealed class DeviceValidator : IDeviceValidator
{
    private readonly ISiteRepository _sites;

    public DeviceValidator(ISiteRepository sites)
    {
        _sites = sites;
    }

    public async Task<string?> ValidateAsync(Device device, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(device.IpAddress) || !IPAddress.TryParse(device.IpAddress, out _))
        {
            return "Địa chỉ IP không hợp lệ.";
        }

        if (device.Status is not ("Online" or "Offline"))
        {
            return "Trạng thái chỉ được nhận giá trị 'Online' hoặc 'Offline'.";
        }

        var siteExists = await _sites.ExistsAsync(device.SiteId, cancellationToken);
        return siteExists ? null : "SiteId không tồn tại.";
    }
}
