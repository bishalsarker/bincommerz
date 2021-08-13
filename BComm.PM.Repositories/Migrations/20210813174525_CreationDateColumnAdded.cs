using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class CreationDateColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "tags",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "products",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "product_tags",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "processes",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "orders",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "order_process_logs",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "order_payment_logs",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "order_items",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "images",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "image_gallery",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "categories",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "product_tags");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "processes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "order_process_logs");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "order_payment_logs");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_om",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "images");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "image_gallery");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "bcomm_pm",
                table: "categories");
        }
    }
}
