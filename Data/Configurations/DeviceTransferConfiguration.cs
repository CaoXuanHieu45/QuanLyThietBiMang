using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Data.Configurations;

public sealed class DeviceTransferConfiguration : IEntityTypeConfiguration<DeviceTransfer>
{
    public void Configure(EntityTypeBuilder<DeviceTransfer> builder)
    {
        builder.HasOne(transfer => transfer.Device)
            .WithMany()
            .HasForeignKey(transfer => transfer.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Site>()
            .WithMany()
            .HasForeignKey(transfer => transfer.FromSiteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Site>()
            .WithMany()
            .HasForeignKey(transfer => transfer.ToSiteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(transfer => new { transfer.DeviceId, transfer.TransferredAtUtc });
    }
}
