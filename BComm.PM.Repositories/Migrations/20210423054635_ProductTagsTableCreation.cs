using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class ProductTagsTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_tags",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagHashId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductHashId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_tags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_tags_ProductHashId",
                schema: "bcomm_pm",
                table: "product_tags",
                column: "ProductHashId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_tags",
                schema: "bcomm_pm");
        }
    }
}
