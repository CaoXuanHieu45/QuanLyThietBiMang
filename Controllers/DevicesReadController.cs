using Microsoft.AspNetCore.Mvc;
using QuanLyThietBiMang.Services;

namespace QuanLyThietBiMang.Controllers;

[Route("api/devices")]
[ApiController]
public sealed class DevicesReadController : ControllerBase
{
    private readonly DeviceQueryService _deviceQueries;

    public DevicesReadController(DeviceQueryService deviceQueries)
    {
        _deviceQueries = deviceQueries;
    }

    [HttpGet]
    public async Task<IActionResult> GetDevices()
    {
        var devices = await _deviceQueries.GetAllAsync();
        return Ok(devices);
    }

    [HttpGet("{id}", Name = "GetDeviceById")]
    public async Task<IActionResult> GetDevice(int id)
    {
        var device = await _deviceQueries.GetByIdAsync(id);
        return device is null ? NotFound() : Ok(device);
    }

    [HttpGet("{id}/transfers")]
    public async Task<IActionResult> GetDeviceTransfers(int id)
    {
        var device = await _deviceQueries.GetByIdAsync(id);
        if (device is null) return NotFound();

        return Ok(await _deviceQueries.GetTransfersAsync(id));
    }
}
