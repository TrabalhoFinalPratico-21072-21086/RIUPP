using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Migrations
{
    public partial class AchoQueComecoAEntenderIsto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonoFK",
                table: "Ficheiros");

            migrationBuilder.DropColumn(
                name: "UtilizadorFK",
                table: "Comentarios");

            migrationBuilder.AddColumn<int>(
                name: "Dono",
                table: "Ficheiros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuemComentou",
                table: "Comentarios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ficheiros_Dono",
                table: "Ficheiros",
                column: "Dono");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_QuemComentou",
                table: "Comentarios",
                column: "QuemComentou");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Utilizadores_QuemComentou",
                table: "Comentarios",
                column: "QuemComentou",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ficheiros_Utilizadores_Dono",
                table: "Ficheiros",
                column: "Dono",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Utilizadores_QuemComentou",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Ficheiros_Utilizadores_Dono",
                table: "Ficheiros");

            migrationBuilder.DropIndex(
                name: "IX_Ficheiros_Dono",
                table: "Ficheiros");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_QuemComentou",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "Dono",
                table: "Ficheiros");

            migrationBuilder.DropColumn(
                name: "QuemComentou",
                table: "Comentarios");

            migrationBuilder.AddColumn<int>(
                name: "DonoFK",
                table: "Ficheiros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UtilizadorFK",
                table: "Comentarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
