using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertAPI.Migrations
{
    public partial class addedMoreData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 4,
                column: "Height",
                value: 11m);

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "IdBuilding", "City", "Height", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { 5, "Poznan", 12m, "Lublinska", 3 },
                    { 6, "Poznan", 9m, "Lublinska", 3 }
                });

            migrationBuilder.UpdateData(
                table: "Campaign",
                keyColumn: "IdCampaign",
                keyValue: 2,
                columns: new[] { "PricePerSquareMeter", "ToIdBuilding" },
                values: new object[] { 3344m, 3 });

            migrationBuilder.InsertData(
                table: "Campaign",
                columns: new[] { "IdCampaign", "EndDate", "FromIdBuilding", "IdClient", "PricePerSquareMeter", "StartDate", "ToIdBuilding" },
                values: new object[] { 3, new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, 3344m, new DateTime(2020, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Campaign",
                keyColumn: "IdCampaign",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 4,
                column: "Height",
                value: 12m);

            migrationBuilder.UpdateData(
                table: "Campaign",
                keyColumn: "IdCampaign",
                keyValue: 2,
                columns: new[] { "PricePerSquareMeter", "ToIdBuilding" },
                values: new object[] { 3141m, 4 });
        }
    }
}
