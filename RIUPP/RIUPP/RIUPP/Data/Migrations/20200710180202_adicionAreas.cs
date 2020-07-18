using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Data.Migrations
{
    public partial class adicionAreas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "Designacao", "Nome" },
                values: new object[,]
                {
                    { 1, "", "Engenharia" },
                    { 2, "", "Artes" },
                    { 3, "", "Direito" },
                    { 4, "", "Economia" },
                    { 5, "", "Gestao" },
                    { 6, "", "Design" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
