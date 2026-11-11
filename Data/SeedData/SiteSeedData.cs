using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Data.SeedData;

public static class SiteSeedData
{
    public static List<Site> Create() =>
    [
        new Site { Name = "Văn phòng chính" },
        new Site { Name = "Phòng lab" }
    ];
}
