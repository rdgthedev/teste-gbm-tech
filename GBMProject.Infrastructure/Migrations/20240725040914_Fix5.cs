using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Origin = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Destiny = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Cargo = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                    table.ForeignKey(
                        name: "DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "TruckId",
                        column: x => x.TruckId,
                        principalTable: "Truck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_DriverId",
                table: "Delivery",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Id",
                table: "Delivery",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_TruckId",
                table: "Delivery",
                column: "TruckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivery");
        }
    }
}
