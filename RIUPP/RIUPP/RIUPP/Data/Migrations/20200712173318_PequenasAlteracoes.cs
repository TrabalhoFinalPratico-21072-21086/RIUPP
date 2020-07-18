using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Data.Migrations
{
    public partial class PequenasAlteracoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Ficheiros");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Utilizadores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Ficheiros",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
