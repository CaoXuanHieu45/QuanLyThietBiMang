using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceTransfer> DeviceTransfers { get; set; } // Bổ sung DbSet cho lịch sử chuyển thiết bị

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
