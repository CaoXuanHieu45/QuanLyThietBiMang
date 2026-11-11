using QuanLyThietBiMang.Models;
using QuanLyThietBiMang.Repositories;

namespace QuanLyThietBiMang.Services;

/// <summary>Chứa các truy vấn đọc thiết bị.</summary>
public sealed class DeviceQueryService
{
    private readonly IUnitOfWork _unitOfWork;

    public DeviceQueryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<DeviceResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await _unitOfWork.Devices.GetAllAsync(cancellationToken))
        .Select(device => new DeviceResponse
        {
            Id = device.Id,
            Name = device.Name,
            IpAddress = device.IpAddress,
            Status = device.Status,
            SiteId = device.SiteId,
            SiteName = device.Site != null ? device.Site.Name : string.Empty
        }).ToList();

    public async Task<DeviceResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var device = await _unitOfWork.Devices.GetByIdAsync(id, cancellationToken);
        return device is null ? null : new DeviceResponse
        {
            Id = device.Id,
            Name = device.Name,
            IpAddress = device.IpAddress,
            Status = device.Status,
            SiteId = device.SiteId,
            SiteName = device.Site != null ? device.Site.Name : string.Empty
        };
    }

    public Task<List<DeviceTransfer>> GetTransfersAsync(int id, CancellationToken cancellationToken = default) =>
        _unitOfWork.DeviceTransfers.GetForDeviceAsync(id, cancellationToken);
}
