using Microsoft.AspNetCore.Mvc;
using QuanLyThietBiMang.Models;
using QuanLyThietBiMang.Services;

namespace QuanLyThietBiMang.Controllers;

[Route("api/devices")]
[ApiController]
public sealed class DevicesWriteController : ControllerBase
{
    private readonly DeviceCommandService _deviceCommands;

    public DevicesWriteController(DeviceCommandService deviceCommands)
    {
        _deviceCommands = deviceCommands;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDevice([FromBody] Device deviceInput)
    {
        var result = await _deviceCommands.CreateAsync(deviceInput);
        if (result.Error is not null)
        {
            return result.Conflict
                ? Conflict(new { message = result.Error })
                : BadRequest(new { message = result.Error });
        }

        return CreatedAtRoute("GetDeviceById", new { id = result.Device!.Id }, result.Device);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDevice(int id, [FromBody] Device deviceInput)
    {
        var result = await _deviceCommands.UpdateAsync(id, deviceInput);
        if (result.NotFound)
        {
            return NotFound();
        }

        if (result.Error is not null)
        {
            return result.Conflict
                ? Conflict(new { message = result.Error })
                : BadRequest(new { message = result.Error });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDevice(int id)
    {
        var result = await _deviceCommands.DeleteAsync(id);
        if (result.NotFound) return NotFound();
        if (result.Conflict) return Conflict(new { message = result.Error });
        return NoContent();
    }

    [HttpPost("{id}/transfers")]
    public async Task<IActionResult> TransferDevice(int id, [FromBody] TransferRequest request)
    {
        var result = await _deviceCommands.TransferAsync(id, request);
        if (result.NotFound)
        {
            return NotFound();
        }

        if (result.Error is not null)
        {
            return result.Conflict
                ? Conflict(new { message = result.Error })
                : BadRequest(new { message = result.Error });
        }

        return StatusCode(201, result.Transfer);
    }
}
