using QuanLyThietBiMang.Repositories;

namespace QuanLyThietBiMang.Data.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(
        AppDbContext context,
        IDeviceRepository devices,
        ISiteRepository sites,
        IDeviceTransferRepository deviceTransfers)
    {
        _context = context;
        Devices = devices;
        Sites = sites;
        DeviceTransfers = deviceTransfers;
    }

    public IDeviceRepository Devices { get; }
    public ISiteRepository Sites { get; }
    public IDeviceTransferRepository DeviceTransfers { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
