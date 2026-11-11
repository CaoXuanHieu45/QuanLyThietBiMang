using QuanLyThietBiMang.Models;
using QuanLyThietBiMang.Repositories;

namespace QuanLyThietBiMang.Services;

/// <summary>Thực hiện các nghiệp vụ ghi của thiết bị.</summary>
public sealed class DeviceCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDeviceValidator _validator;

    public DeviceCommandService(IUnitOfWork unitOfWork, IDeviceValidator validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<DeviceWriteResult> CreateAsync(Device input, CancellationToken cancellationToken = default)
    {
        var error = await _validator.ValidateAsync(input, cancellationToken);
        if (error is not null) return DeviceWriteResult.ValidationError(error);

        var device = new Device
        {
            Name = input.Name.Trim(),
            IpAddress = input.IpAddress.Trim(),
            Status = input.Status,
            SiteId = input.SiteId
        };

        _unitOfWork.Devices.Add(device);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return DeviceWriteResult.Success(device);
    }

    public async Task<DeviceWriteResult> UpdateAsync(int id, Device input, CancellationToken cancellationToken = default)
    {
        var device = await _unitOfWork.Devices.GetByIdAsync(id, cancellationToken);
        if (device is null) return DeviceWriteResult.Missing();

        var error = await _validator.ValidateAsync(input, cancellationToken);
        if (error is not null) return DeviceWriteResult.ValidationError(error);
        if (device.SiteId != input.SiteId)
            return DeviceWriteResult.ConflictError("Hãy dùng API chuyển địa điểm để lưu lịch sử điều chuyển.");

        device.Name = input.Name.Trim();
        device.IpAddress = input.IpAddress.Trim();
        device.Status = input.Status;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return DeviceWriteResult.Success(device);
    }

    public async Task<DeviceWriteResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var device = await _unitOfWork.Devices.GetByIdAsync(id, cancellationToken);
        if (device is null) return DeviceWriteResult.Missing();

        if (await _unitOfWork.DeviceTransfers.ExistsForDeviceAsync(id, cancellationToken))
            return DeviceWriteResult.ConflictError("Không thể xóa thiết bị đã có lịch sử điều chuyển.");

        _unitOfWork.Devices.Remove(device);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return DeviceWriteResult.Success();
    }

    public async Task<DeviceWriteResult> TransferAsync(int id, TransferRequest request, CancellationToken cancellationToken = default)
    {
        var device = await _unitOfWork.Devices.GetByIdAsync(id, cancellationToken);
        if (device is null) return DeviceWriteResult.Missing();

        if (!await _unitOfWork.Sites.ExistsAsync(request.ToSiteId, cancellationToken))
            return DeviceWriteResult.ValidationError("Địa điểm đích không tồn tại.");

        if (device.SiteId == request.ToSiteId)
            return DeviceWriteResult.ConflictError("Địa điểm đích đang là địa điểm hiện tại.");

        var transfer = new DeviceTransfer
        {
            DeviceId = device.Id,
            FromSiteId = device.SiteId,
            ToSiteId = request.ToSiteId,
            TransferredAtUtc = DateTime.UtcNow
        };

        device.SiteId = request.ToSiteId;
        _unitOfWork.DeviceTransfers.Add(transfer);
        // Một lần SaveChanges duy nhất để cập nhật địa điểm và lịch sử trong cùng transaction EF Core.
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return DeviceWriteResult.Success(device, transfer);
    }
}
