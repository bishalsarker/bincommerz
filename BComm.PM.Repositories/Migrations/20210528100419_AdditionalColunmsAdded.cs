using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class AdditionalColunmsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                schema: "bcomm_pm",
                table: "products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "bcomm_pm",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentNotes",
                schema: "bcomm_om",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedOn",
                schema: "bcomm_pm",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "bcomm_pm",
                table: "products");

            migrationBuilder.DropColumn(
                name: "PaymentNotes",
                schema: "bcomm_om",
                table: "orders");
        }
    }
}
