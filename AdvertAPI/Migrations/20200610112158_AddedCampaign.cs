using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertAPI.Migrations
{
    public partial class AddedCampaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Campaign",
                columns: new[] { "IdCampaign", "EndDate", "FromIdBuilding", "IdClient", "PricePerSquareMeter", "StartDate", "ToIdBuilding" },
                values: new object[] { 2, new DateTime(2020, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 3141m, new DateTime(2020, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Campaign",
                keyColumn: "IdCampaign",
                keyValue: 2);
        }
    }
}
