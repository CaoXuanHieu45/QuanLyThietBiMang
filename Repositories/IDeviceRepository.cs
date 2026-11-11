using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Repositories;

public interface IDeviceRepository
{
    Task<List<Device>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Device?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void Add(Device device);
    void Remove(Device device);
}
