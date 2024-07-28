using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Truck",
                newName: "YearOfManifacture");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearOfManifacture",
                table: "Truck",
                newName: "CreatedAt");
        }
    }
}
