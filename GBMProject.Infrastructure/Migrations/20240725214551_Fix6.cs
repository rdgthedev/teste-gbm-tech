using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Truck_DriverId",
                table: "Truck");

            migrationBuilder.DropIndex(
                name: "IX_Truck_DriverId",
                table: "Truck");

            migrationBuilder.DropIndex(
                name: "IX_Driver_Cpf",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_Id",
                table: "Driver");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Truck");

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

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Driver",
                type: "VARCHAR(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Driver",
                type: "VARCHAR(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Deliveries",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Deliveries",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries",
                column: "Id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Driver_Cpf",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_Id",
                table: "Driver");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Status",
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

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "Truck",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Driver",
                type: "NVARCHAR(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Driver",
                type: "NVARCHAR(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Delivery",
                type: "NVARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_DriverId",
                table: "Truck",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_Cpf",
                table: "Driver",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_Id",
                table: "Driver",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Truck_DriverId",
                table: "Truck",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
