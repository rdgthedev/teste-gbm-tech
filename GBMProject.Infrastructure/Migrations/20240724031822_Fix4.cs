using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Truck_DriverId",
                table: "Truck");

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "Truck",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_DriverId",
                table: "Truck",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Truck_DriverId",
                table: "Truck");

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "Truck",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Truck_DriverId",
                table: "Truck",
                column: "DriverId",
                unique: true);
        }
    }
}
