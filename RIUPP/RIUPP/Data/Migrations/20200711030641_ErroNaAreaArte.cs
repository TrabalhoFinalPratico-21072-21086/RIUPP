using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Data.Migrations
{
    public partial class ErroNaAreaArte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Designacao",
                value: "A arte é a autoexpressão lutando para ser absoluta.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Designacao",
                value: "A competitividade de um país não começa nas indústrias ou nos laboratórios de engenharia. Ela começa na sala de aula.");
        }
    }
}
