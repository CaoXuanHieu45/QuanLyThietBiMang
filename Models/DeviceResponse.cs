namespace QuanLyThietBiMang.Models;

/// <summary>Thông tin thiết bị trả về từ các API đọc.</summary>
public sealed class DeviceResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string IpAddress { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public int SiteId { get; init; }
    public string SiteName { get; init; } = string.Empty;
}
