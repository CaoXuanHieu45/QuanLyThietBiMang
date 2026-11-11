using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Services;

public sealed record DeviceWriteResult(
    Device? Device,
    DeviceTransfer? Transfer,
    string? Error,
    bool NotFound,
    bool Conflict)
{
    public static DeviceWriteResult Success(Device? device = null, DeviceTransfer? transfer = null) =>
        new(device, transfer, null, false, false);

    public static DeviceWriteResult ValidationError(string error) =>
        new(null, null, error, false, false);

    public static DeviceWriteResult ConflictError(string error) =>
        new(null, null, error, false, true);

    public static DeviceWriteResult Missing() => new(null, null, null, true, false);
}
