using Microsoft.EntityFrameworkCore.Migrations;

namespace BComm.PM.Repositories.Migrations
{
    public partial class CouponDiscountTypesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                schema: "bcomm_om",
                table: "coupons",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<double>(
                name: "MinimumPurchaseAmount",
                schema: "bcomm_om",
                table: "coupons",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountType",
                schema: "bcomm_om",
                table: "coupons");

            migrationBuilder.DropColumn(
                name: "MinimumPurchaseAmount",
                schema: "bcomm_om",
                table: "coupons");
        }
    }
}
