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
            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Cpf = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    CnhCategory = table.Column<string>(type: "VARCHAR(1)", maxLength: 1, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Plate = table.Column<string>(type: "VARCHAR(7)", maxLength: 7, nullable: false),
                    Model = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    YearOfManifacture = table.Column<int>(type: "INT", nullable: false),
                    Color = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    NumberOfAxles = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Origin = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Destiny = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Cargo = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Status = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Driver_Cpf",
                table: "Driver",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Driver_Id",
                table: "Driver",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Truck_Id",
                table: "Truck",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_Plate",
                table: "Truck",
                column: "Plate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Truck");
        }
    }
}
