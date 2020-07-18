using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Data.Migrations
{
    public partial class NovosDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "Comentarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comentarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comentarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comentarios",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comentarios",
                keyColumn: "Id",
                keyValue: 5);

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

            migrationBuilder.DeleteData(
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ficheiros",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ficheiros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ficheiros",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 6);

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
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Utilizadores",
                keyColumn: "Id",
                keyValue: 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Utilizadores",
                columns: new[] { "Id", "Aut", "Email", "Foto", "Nome" },
                values: new object[,]
                {
                    { 1, null, "Luis@ipt.pt", "foto.png", "Luís Freitas" },
                    { 2, null, "Andreia@ipt.pt", "foto.png", "Andreia Gomes" },
                    { 3, null, "Cristina@ipt.pt", "foto.png", "Cristina Sousa" },
                    { 4, null, "Sonia@ipt.pt", "foto.png", "Sónia Rosa" },
                    { 5, null, "Antonio@ipt.pt", "foto.png", "António Santos" },
                    { 6, null, "Gustavo@ipt.pt", "foto.png", "Gustavo Alves" },
                    { 7, null, "Rosa@ipt.pt", "foto.png", "Rosa Vieira" },
                    { 8, null, "Daniel@ipt.pt", "foto.png", "Daniel Dias" },
                    { 9, null, "Tania@ipt.pt", "foto.png", "Tânia Gomes" },
                    { 10, null, "AndreiaG@ipt.pt", "foto.png", "Andreia Correia" }
                });

            migrationBuilder.InsertData(
                table: "Ficheiros",
                columns: new[] { "Id", "AreaFK", "DateUpload", "Descricao", "Dono", "Local", "Observacao", "Tipo", "Titulo" },
                values: new object[] { 1, 1, new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pequena Descrição", 4, "", "nenhuma", "", "Documento1" });

            migrationBuilder.InsertData(
                table: "Ficheiros",
                columns: new[] { "Id", "AreaFK", "DateUpload", "Descricao", "Dono", "Local", "Observacao", "Tipo", "Titulo" },
                values: new object[] { 2, 2, new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pequena Descrição", 7, "", "nenhuma", "", "Documento2" });

            migrationBuilder.InsertData(
                table: "Ficheiros",
                columns: new[] { "Id", "AreaFK", "DateUpload", "Descricao", "Dono", "Local", "Observacao", "Tipo", "Titulo" },
                values: new object[] { 3, 3, new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pequena Descrição", 8, "", "nenhuma", "", "Documento3" });

            migrationBuilder.InsertData(
                table: "Comentarios",
                columns: new[] { "Id", "Coment", "Date", "FicheiroFK", "QuemComentou", "Visivel" },
                values: new object[,]
                {
                    { 1, "primeiroComentario", new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "S" },
                    { 4, "quartoComentario", new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, "S" },
                    { 2, "segundoComentario", new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "S" },
                    { 5, "quintoComentario", new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, "S" },
                    { 3, "terceiroComentario", new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "S" }
                });

            migrationBuilder.InsertData(
                table: "Downloads",
                columns: new[] { "Id", "Data", "FicheiroFK", "UtilizadorFK" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 4, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 },
                    { 2, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 5, new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 }
                });
        }
    }
}
