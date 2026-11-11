using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThietBiMang.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceTransfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    FromSiteId = table.Column<int>(type: "int", nullable: false),
                    ToSiteId = table.Column<int>(type: "int", nullable: false),
                    TransferredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTransfers_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTransfers_DeviceId_TransferredAtUtc",
                table: "DeviceTransfers",
                columns: new[] { "DeviceId", "TransferredAtUtc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceTransfers");
        }
    }
}
