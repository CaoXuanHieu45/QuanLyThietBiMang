using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Services;

/// <summary>Kết quả thao tác ghi để controller chuyển thành HTTP response.</summary>
public sealed record DeviceWriteResult(Device? Device, string? Error, bool NotFound)
{
    public static DeviceWriteResult Success(Device? device = null) => new(device, null, false);
    public static DeviceWriteResult ValidationError(string error) => new(null, error, false);
    public static DeviceWriteResult Missing() => new(null, null, true);
}
