using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Services;

public interface IDeviceValidator
{
    Task<string?> ValidateAsync(Device device, CancellationToken cancellationToken = default);
}
