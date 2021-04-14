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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tags",
                schema: "bcomm_pm");
        }
    }
}
