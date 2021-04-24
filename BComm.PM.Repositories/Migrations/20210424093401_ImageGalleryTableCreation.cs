using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class ImageGalleryTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "image_gallery",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HashId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image_gallery", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_image_gallery_HashId",
                schema: "bcomm_pm",
                table: "image_gallery",
                column: "HashId",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_image_gallery_ProductId",
                schema: "bcomm_pm",
                table: "image_gallery",
                column: "ProductId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "image_gallery",
                schema: "bcomm_pm");
        }
    }
}
