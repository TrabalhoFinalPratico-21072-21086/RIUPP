using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Migrations
{
    public partial class AlteracaoDeNome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Utilizadores");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Utilizadores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Utilizadores");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Utilizadores",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
