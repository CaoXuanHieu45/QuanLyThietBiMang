using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Repositories;

public interface ISiteRepository
{
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Site>> GetAllAsync(CancellationToken cancellationToken = default);
}
