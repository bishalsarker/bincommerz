using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class DnsPropertiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cname",
                schema: "bcomm_user",
                table: "url_mappings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CnameId",
                schema: "bcomm_user",
                table: "url_mappings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DnsId",
                schema: "bcomm_user",
                table: "url_mappings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cname",
                schema: "bcomm_user",
                table: "url_mappings");

            migrationBuilder.DropColumn(
                name: "CnameId",
                schema: "bcomm_user",
                table: "url_mappings");

            migrationBuilder.DropColumn(
                name: "DnsId",
                schema: "bcomm_user",
                table: "url_mappings");
        }
    }
}
