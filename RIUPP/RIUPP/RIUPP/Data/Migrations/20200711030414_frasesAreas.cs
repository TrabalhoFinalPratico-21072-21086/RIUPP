using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Data.Migrations
{
    public partial class frasesAreas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Designacao",
                value: "A competitividade de um país não começa nas indústrias ou nos laboratórios de engenharia. Ela começa na sala de aula.");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Designacao",
                value: "A competitividade de um país não começa nas indústrias ou nos laboratórios de engenharia. Ela começa na sala de aula.");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Designacao",
                value: "A força do direito deve superar o direito da força.");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 4,
                column: "Designacao",
                value: "A economia é uma virtude distributiva e consiste não em poupar mas em escolher.");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5,
                column: "Designacao",
                value: "Gestão é substituir músculos por pensamentos, folclore e superstição por conhecimento, e força por cooperação.");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 6,
                column: "Designacao",
                value: "Quinze anos atrás, as empresas competiam em preço. Hoje em qualidade. Amanhã será no design.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Designacao",
                value: "");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Designacao",
                value: "");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Designacao",
                value: "");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 4,
                column: "Designacao",
                value: "");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5,
                column: "Designacao",
                value: "");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 6,
                column: "Designacao",
                value: "");
        }
    }
}
