using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Data;
using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Services;

/// <summary>Chứa các thao tác tạo, cập nhật và xóa thiết bị.</summary>
public sealed class DeviceCommandService
{
    private readonly AppDbContext _context;
    private readonly DeviceValidator _validator;

    public DeviceCommandService(AppDbContext context, DeviceValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<DeviceWriteResult> CreateAsync(Device input)
    {
        var error = await _validator.ValidateAsync(input);
        if (error is not null)
        {
            return DeviceWriteResult.ValidationError(error);
        }

        var device = new Device
        {
            Name = input.Name,
            IpAddress = input.IpAddress,
            Status = input.Status,
            SiteId = input.SiteId
        };

        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        return DeviceWriteResult.Success(device);
    }

    public async Task<DeviceWriteResult> UpdateAsync(int id, Device input)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device is null)
        {
            return DeviceWriteResult.Missing();
        }

        var error = await _validator.ValidateAsync(input);
        if (error is not null)
        {
            return DeviceWriteResult.ValidationError(error);
        }

        device.Name = input.Name;
        device.IpAddress = input.IpAddress;
        device.Status = input.Status;
        device.SiteId = input.SiteId;
        await _context.SaveChangesAsync();

        return DeviceWriteResult.Success();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device is null)
        {
            return false;
        }

        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
        return true;
    }
}
