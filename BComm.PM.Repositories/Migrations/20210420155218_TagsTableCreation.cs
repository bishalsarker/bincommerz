using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class TagsTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bcomm_pm");

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "bcomm_pm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HashId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tags_HashId",
                schema: "bcomm_pm",
                table: "tags",
                column: "HashId",
                unique: true,
                filter: "[HashId] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_tags_ShopId",
                schema: "bcomm_pm",
                table: "tags",
                column: "ShopId",
                unique: false,
                filter: "[ShopId] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tags",
                schema: "bcomm_pm");
        }
    }
}
