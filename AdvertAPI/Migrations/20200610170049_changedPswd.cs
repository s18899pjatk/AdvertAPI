using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertAPI.Migrations
{
    public partial class changedPswd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "IdClient",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "B7KqEYl86BI7WHWSGM7Vv0tuYwPz1lc7p9egQonkkAQ=", "vrCE57covNR7T2gk3E6XLw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "IdClient",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "dsa", "dsadsa" });
        }
    }
}
