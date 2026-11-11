namespace QuanLyThietBiMang.Repositories;

/// <summary>Application contract for coordinating repositories and saving one unit of work.</summary>
public interface IUnitOfWork
{
    IDeviceRepository Devices { get; }
    ISiteRepository Sites { get; }
    IDeviceTransferRepository DeviceTransfers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
