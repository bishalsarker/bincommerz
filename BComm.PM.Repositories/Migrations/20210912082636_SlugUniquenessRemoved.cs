using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class SlugUniquenessRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pages_Slug",
                schema: "bcomm_cm",
                table: "pages");

            migrationBuilder.DropIndex(
                name: "IX_categories_Slug",
                schema: "bcomm_pm",
                table: "categories");

            migrationBuilder.CreateIndex(
                name: "IX_pages_Slug",
                schema: "bcomm_cm",
                table: "pages",
                column: "Slug")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_categories_Slug",
                schema: "bcomm_pm",
                table: "categories",
                column: "Slug")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_pages_Slug",
                schema: "bcomm_cm",
                table: "pages");

            migrationBuilder.DropIndex(
                name: "IX_categories_Slug",
                schema: "bcomm_pm",
                table: "categories");

            migrationBuilder.CreateIndex(
                name: "IX_pages_Slug",
                schema: "bcomm_cm",
                table: "pages",
                column: "Slug",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_categories_Slug",
                schema: "bcomm_pm",
                table: "categories",
                column: "Slug",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }
    }
}
