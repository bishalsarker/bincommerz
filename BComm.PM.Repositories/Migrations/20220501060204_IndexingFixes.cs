using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class IndexingFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_url_mappings_ShopId",
                schema: "bcomm_user",
                table: "url_mappings");

            migrationBuilder.DropIndex(
                name: "IX_url_mappings_UrlMapType",
                schema: "bcomm_user",
                table: "url_mappings");

            migrationBuilder.CreateIndex(
                name: "IX_url_mappings_ShopId",
                schema: "bcomm_user",
                table: "url_mappings",
                column: "ShopId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_url_mappings_UrlMapType",
                schema: "bcomm_user",
                table: "url_mappings",
                column: "UrlMapType")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_url_mappings_ShopId",
                schema: "bcomm_user",
                table: "url_mappings");

            migrationBuilder.DropIndex(
                name: "IX_url_mappings_UrlMapType",
                schema: "bcomm_user",
                table: "url_mappings");

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
    }
}
