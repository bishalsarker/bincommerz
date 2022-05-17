using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class SubscriptionTableUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfChargedInvoices",
                schema: "bcomm_user",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "NumberOfInvoices",
                schema: "bcomm_user",
                table: "subscriptions");

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTill",
                schema: "bcomm_user",
                table: "subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidTill",
                schema: "bcomm_user",
                table: "subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChargedInvoices",
                schema: "bcomm_user",
                table: "subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfInvoices",
                schema: "bcomm_user",
                table: "subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
