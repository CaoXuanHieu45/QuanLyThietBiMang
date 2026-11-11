using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Data.Configurations;

public sealed class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasOne(device => device.Site)
            .WithMany(site => site.Devices)
            .HasForeignKey(device => device.SiteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(table => table.HasCheckConstraint(
            "CK_Devices_Status",
            "[Status] IN ('Online', 'Offline')"));
    }
}
