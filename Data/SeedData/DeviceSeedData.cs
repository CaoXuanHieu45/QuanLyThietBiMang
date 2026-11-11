using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Data.SeedData;

public static class DeviceSeedData
{
    public static List<Device> Create(IReadOnlyList<Site> sites)
    {
        if (sites.Count < 2)
        {
            throw new ArgumentException("Cần có ít nhất hai địa điểm để tạo dữ liệu mẫu thiết bị.", nameof(sites));
        }

        return
        [
            new Device { Name = "Router Core", IpAddress = "192.168.1.1", Status = "Online", SiteId = sites[0].Id },
            new Device { Name = "Switch Tầng 2", IpAddress = "192.168.10.12", Status = "Online", SiteId = sites[0].Id },
            new Device { Name = "Access Point Lab", IpAddress = "192.168.20.5", Status = "Offline", SiteId = sites[1].Id }
        ];
    }
}
