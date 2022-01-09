using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertisementApp.Persistence.Migrations
{
    public partial class AddPriceAndImageToAdvertisementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Advertisements",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Advertisements",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Advertisements");
        }
    }
}
