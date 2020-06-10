using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertAPI.Migrations
{
    public partial class AddedBanners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Banner",
                columns: new[] { "IdAdvertisement", "Area", "IdCampaign", "Name", "Price" },
                values: new object[] { 4, 2.1m, 2, "Donutik", 311m });

            migrationBuilder.InsertData(
                table: "Banner",
                columns: new[] { "IdAdvertisement", "Area", "IdCampaign", "Name", "Price" },
                values: new object[] { 5, 1.9m, 2, "McDonalds", 566m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banner",
                keyColumn: "IdAdvertisement",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Banner",
                keyColumn: "IdAdvertisement",
                keyValue: 5);
        }
    }
}
