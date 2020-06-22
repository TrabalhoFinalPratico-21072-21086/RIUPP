using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Migrations
{
    public partial class EsquecimeDoEMail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Utilizadores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Utilizadores");
        }
    }
}
