using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Models;

namespace QuanLyThietBiMang.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Site)
                .WithMany(s => s.Devices)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Device>()
                .ToTable(table => table.HasCheckConstraint(
                    "CK_Devices_Status",
                    "[Status] IN ('Online', 'Offline')"));
        }
    }
}
