using QuanLyThietBiMang.Data.SeedData;

namespace QuanLyThietBiMang.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Sites.Any())
        {
            return;
        }

        var sites = SiteSeedData.Create();
        context.Sites.AddRange(sites);
        context.SaveChanges();

        var devices = DeviceSeedData.Create(sites);
        context.Devices.AddRange(devices);
        context.SaveChanges();
    }
}
