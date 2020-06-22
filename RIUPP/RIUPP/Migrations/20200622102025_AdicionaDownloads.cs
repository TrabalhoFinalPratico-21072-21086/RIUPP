using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Migrations
{
    public partial class AdicionaDownloads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Downloads",
                columns: new[] { "Id", "Data", "FicheiroFK", "UtilizadorFK" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 4, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 },
                    { 5, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Downloads",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Downloads",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Downloads",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Downloads",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Downloads",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
