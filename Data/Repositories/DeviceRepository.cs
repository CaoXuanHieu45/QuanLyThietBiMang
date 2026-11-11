using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Models;
using QuanLyThietBiMang.Repositories;

namespace QuanLyThietBiMang.Data.Repositories;

public sealed class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _context;

    public DeviceRepository(AppDbContext context) => _context = context;

    public Task<List<Device>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _context.Devices.AsNoTracking().Include(device => device.Site).ToListAsync(cancellationToken);

    public Task<Device?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _context.Devices.Include(device => device.Site)
            .FirstOrDefaultAsync(device => device.Id == id, cancellationToken);

    public void Add(Device device) => _context.Devices.Add(device);
    public void Remove(Device device) => _context.Devices.Remove(device);
}
