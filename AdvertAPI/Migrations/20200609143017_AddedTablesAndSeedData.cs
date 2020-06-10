using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertAPI.Migrations
{
    public partial class AddedTablesAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    IdBuilding = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(maxLength: 100, nullable: false),
                    StreetNumber = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.IdBuilding);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Phone = table.Column<string>(maxLength: 100, nullable: false),
                    Login = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    RefreshToken = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    IdCampaign = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PricePerSquareMeter = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    FromIdBuilding = table.Column<int>(nullable: false),
                    ToIdBuilding = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.IdCampaign);
                    table.ForeignKey(
                        name: "FK_Campaign_Building_FromIdBuilding",
                        column: x => x.FromIdBuilding,
                        principalTable: "Building",
                        principalColumn: "IdBuilding");
                    table.ForeignKey(
                        name: "FK_Campaign_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Campaign_Building_ToIdBuilding",
                        column: x => x.ToIdBuilding,
                        principalTable: "Building",
                        principalColumn: "IdBuilding");
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    IdAdvertisement = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IdCampaign = table.Column<int>(nullable: false),
                    Area = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.IdAdvertisement);
                    table.ForeignKey(
                        name: "FK_Banner_Campaign_IdCampaign",
                        column: x => x.IdCampaign,
                        principalTable: "Campaign",
                        principalColumn: "IdCampaign",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "IdBuilding", "City", "Height", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "Warsaw", 16.5m, "Krucza", 21 },
                    { 2, "Warsaw", 10m, "Zlota", 51 },
                    { 3, "Krakow", 7m, "Stara", 2 },
                    { 4, "Poznan", 12m, "Lublinska", 3 }
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "IdClient", "Email", "FirstName", "LastName", "Login", "Password", "Phone", "RefreshToken", "Salt" },
                values: new object[] { 1, "badsa@gmail.com", "Bobby", "Broody", "dafasf21", "dsa", "+43131234", "asdfaafasfscsac-fsaf", "dsadsa" });

            migrationBuilder.InsertData(
                table: "Campaign",
                columns: new[] { "IdCampaign", "EndDate", "FromIdBuilding", "IdClient", "PricePerSquareMeter", "StartDate", "ToIdBuilding" },
                values: new object[] { 1, new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 3344m, new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.InsertData(
                table: "Banner",
                columns: new[] { "IdAdvertisement", "Area", "IdCampaign", "Name", "Price" },
                values: new object[] { 1, 1.2m, 1, "Coca-Cola", 2333m });

            migrationBuilder.InsertData(
                table: "Banner",
                columns: new[] { "IdAdvertisement", "Area", "IdCampaign", "Name", "Price" },
                values: new object[] { 2, 1.6m, 1, "Sprite", 1331m });

            migrationBuilder.InsertData(
                table: "Banner",
                columns: new[] { "IdAdvertisement", "Area", "IdCampaign", "Name", "Price" },
                values: new object[] { 3, 1m, 1, "Rollin", 413m });

            migrationBuilder.CreateIndex(
                name: "IX_Banner_IdCampaign",
                table: "Banner",
                column: "IdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_FromIdBuilding",
                table: "Campaign",
                column: "FromIdBuilding");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_IdClient",
                table: "Campaign",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_ToIdBuilding",
                table: "Campaign",
                column: "ToIdBuilding");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
