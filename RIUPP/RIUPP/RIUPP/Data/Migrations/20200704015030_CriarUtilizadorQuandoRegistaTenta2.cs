using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Data.Migrations
{
    public partial class CriarUtilizadorQuandoRegistaTenta2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aut",
                table: "Utilizadores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aut",
                table: "Utilizadores");
        }
    }
}
