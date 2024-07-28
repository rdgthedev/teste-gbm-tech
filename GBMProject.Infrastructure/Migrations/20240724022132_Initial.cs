using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Plate = table.Column<string>(type: "VARCHAR(7)", maxLength: 7, nullable: false),
                    Model = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Color = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    NumberOfAxles = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR(11)", maxLength: 11, nullable: false),
                    CnhCategory = table.Column<string>(type: "VARCHAR", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Celular = table.Column<string>(type: "NVARCHAR(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Truck_DriverId",
                        column: x => x.Id,
                        principalTable: "Truck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Driver_Cpf",
                table: "Driver",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_Id",
                table: "Driver",
                column: "Id");

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
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Truck");
        }
    }
}
