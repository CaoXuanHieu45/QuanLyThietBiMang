namespace QuanLyThietBiMang.Models;

public class DeviceTransfer
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public Device? Device { get; set; }

    public int FromSiteId { get; set; }
    public int ToSiteId { get; set; }

    public DateTime TransferredAtUtc { get; set; } = DateTime.UtcNow;
}