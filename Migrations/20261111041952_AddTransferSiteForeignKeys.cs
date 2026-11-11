using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThietBiMang.Migrations
{
    /// <inheritdoc />
    public partial class AddTransferSiteForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DeviceTransfers_FromSiteId",
                table: "DeviceTransfers",
                column: "FromSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTransfers_ToSiteId",
                table: "DeviceTransfers",
                column: "ToSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTransfers_Sites_FromSiteId",
                table: "DeviceTransfers",
                column: "FromSiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTransfers_Sites_ToSiteId",
                table: "DeviceTransfers",
                column: "ToSiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTransfers_Sites_FromSiteId",
                table: "DeviceTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTransfers_Sites_ToSiteId",
                table: "DeviceTransfers");

            migrationBuilder.DropIndex(
                name: "IX_DeviceTransfers_FromSiteId",
                table: "DeviceTransfers");

            migrationBuilder.DropIndex(
                name: "IX_DeviceTransfers_ToSiteId",
                table: "DeviceTransfers");
        }
    }
}
