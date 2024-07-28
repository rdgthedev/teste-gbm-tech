using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Truck_DriverId",
                table: "Driver");

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "Truck",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Truck_DriverId",
                table: "Truck",
                column: "DriverId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Truck_DriverId",
                table: "Truck",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Truck_DriverId",
                table: "Truck");

            migrationBuilder.DropIndex(
                name: "IX_Truck_DriverId",
                table: "Truck");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Truck");

            migrationBuilder.AddForeignKey(
                name: "FK_Truck_DriverId",
                table: "Driver",
                column: "Id",
                principalTable: "Truck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
