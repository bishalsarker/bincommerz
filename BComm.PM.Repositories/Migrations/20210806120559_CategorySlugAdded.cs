using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class CategorySlugAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "bcomm_pm",
                table: "categories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_categories_Slug",
                schema: "bcomm_pm",
                table: "categories",
                column: "Slug",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_categories_Slug",
                schema: "bcomm_pm",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "bcomm_pm",
                table: "categories");
        }
    }
}
