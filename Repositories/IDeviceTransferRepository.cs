using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Repositories;

public interface IDeviceTransferRepository
{
    void Add(DeviceTransfer transfer);
    Task<bool> ExistsForDeviceAsync(int deviceId, CancellationToken cancellationToken = default);
    Task<List<DeviceTransfer>> GetForDeviceAsync(int deviceId, CancellationToken cancellationToken = default);
}
