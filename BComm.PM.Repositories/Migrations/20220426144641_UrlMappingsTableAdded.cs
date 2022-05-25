using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class UrlMappingsTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "url_mappings",
                schema: "bcomm_user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UrlMapType = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HashId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_url_mappings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_url_mappings_HashId",
                schema: "bcomm_user",
                table: "url_mappings",
                column: "HashId",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_url_mappings_ShopId",
                schema: "bcomm_user",
                table: "url_mappings",
                column: "ShopId",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_url_mappings_UrlMapType",
                schema: "bcomm_user",
                table: "url_mappings",
                column: "UrlMapType",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "url_mappings",
                schema: "bcomm_user");
        }
    }
}
