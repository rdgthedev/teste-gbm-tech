using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameColumnFromPhoneToCellPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Driver",
                newName: "CellPhone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CellPhone",
                table: "Driver",
                newName: "Phone");
        }
    }
}
