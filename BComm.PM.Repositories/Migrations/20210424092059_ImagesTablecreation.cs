using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class ImagesTablecreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "images",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Directory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HashId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_images_HashId",
                schema: "bcomm_pm",
                table: "images",
                column: "HashId",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images",
                schema: "bcomm_pm");
        }
    }
}
