using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Data.Migrations
{
    public partial class beris : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5,
                column: "Nome",
                value: "Gestão");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5,
                column: "Nome",
                value: "Gestao");
        }
    }
}
