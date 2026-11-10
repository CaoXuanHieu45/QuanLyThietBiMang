using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Sites.Any())
            {
                return;
            }

            var sites = new Site[]
            {
                new Site { Name = "Văn phòng chính" },
                new Site { Name = "Phòng lab" }
            };
            context.Sites.AddRange(sites);
            context.SaveChanges();

            var devices = new Device[]
            {
                new Device { Name = "Router Core", IpAddress = "192.168.1.1", Status = "Online", SiteId = sites[0].Id },
                new Device { Name = "Switch Tầng 2", IpAddress = "192.168.10.12", Status = "Online", SiteId = sites[0].Id },
                new Device { Name = "Access Point Lab", IpAddress = "192.168.20.5", Status = "Offline", SiteId = sites[1].Id }
            };
            context.Devices.AddRange(devices);
            context.SaveChanges();
        }
    }
}
