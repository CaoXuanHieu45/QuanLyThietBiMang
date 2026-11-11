using QuanLyThietBiMang.Models;
using QuanLyThietBiMang.Repositories;
using Microsoft.EntityFrameworkCore;

namespace QuanLyThietBiMang.Data.Repositories;

public sealed class DeviceTransferRepository : IDeviceTransferRepository
{
    private readonly AppDbContext _context;

    public DeviceTransferRepository(AppDbContext context) => _context = context;

    public void Add(DeviceTransfer transfer) => _context.DeviceTransfers.Add(transfer);

    public Task<bool> ExistsForDeviceAsync(int deviceId, CancellationToken cancellationToken = default) =>
        _context.DeviceTransfers.AnyAsync(transfer => transfer.DeviceId == deviceId, cancellationToken);

    public Task<List<DeviceTransfer>> GetForDeviceAsync(int deviceId, CancellationToken cancellationToken = default) =>
        _context.DeviceTransfers.AsNoTracking()
            .Where(transfer => transfer.DeviceId == deviceId)
            .OrderByDescending(transfer => transfer.TransferredAtUtc)
            .ToListAsync(cancellationToken);
}
