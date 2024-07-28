using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries");

            migrationBuilder.RenameTable(
                name: "Deliveries",
                newName: "Delivery");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_TruckId",
                table: "Delivery",
                newName: "IX_Delivery_TruckId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_DriverId",
                table: "Delivery",
                newName: "IX_Delivery_DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery");

            migrationBuilder.RenameTable(
                name: "Delivery",
                newName: "Deliveries");

            migrationBuilder.RenameIndex(
                name: "IX_Delivery_TruckId",
                table: "Deliveries",
                newName: "IX_Deliveries_TruckId");

            migrationBuilder.RenameIndex(
                name: "IX_Delivery_DriverId",
                table: "Deliveries",
                newName: "IX_Deliveries_DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries",
                column: "Id");
        }
    }
}
