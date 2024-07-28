using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CnhCategory",
                table: "Driver",
                type: "VARCHAR(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CnhCategory",
                table: "Driver",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1)",
                oldMaxLength: 1);
        }
    }
}
