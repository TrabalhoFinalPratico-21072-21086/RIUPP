using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RIUPP.Data.Migrations
{
    public partial class ModulePlusSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Designacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ficheiros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true),
                    Local = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    DateUpload = table.Column<DateTime>(nullable: false),
                    Dono = table.Column<int>(nullable: false),
                    AreaFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ficheiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ficheiros_Areas_AreaFK",
                        column: x => x.AreaFK,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ficheiros_Utilizadores_Dono",
                        column: x => x.Dono,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coment = table.Column<string>(nullable: true),
                    Visivel = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    QuemComentou = table.Column<int>(nullable: false),
                    FicheiroFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Ficheiros_FicheiroFK",
                        column: x => x.FicheiroFK,
                        principalTable: "Ficheiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Utilizadores_QuemComentou",
                        column: x => x.QuemComentou,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Downloads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    FicheiroFK = table.Column<int>(nullable: false),
                    UtilizadorFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Downloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Downloads_Ficheiros_FicheiroFK",
                        column: x => x.FicheiroFK,
                        principalTable: "Ficheiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Downloads_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

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
                columns: new[] { "Id", "Email", "Foto", "Nome" },
                values: new object[,]
                {
                    { 1, "Luis@ipt.pt", "foto.png", "Luís Freitas" },
                    { 2, "Andreia@ipt.pt", "foto.png", "Andreia Gomes" },
                    { 3, "Cristina@ipt.pt", "foto.png", "Cristina Sousa" },
                    { 4, "Sonia@ipt.pt", "foto.png", "Sónia Rosa" },
                    { 5, "Antonio@ipt.pt", "foto.png", "António Santos" },
                    { 6, "Gustavo@ipt.pt", "foto.png", "Gustavo Alves" },
                    { 7, "Rosa@ipt.pt", "foto.png", "Rosa Vieira" },
                    { 8, "Daniel@ipt.pt", "foto.png", "Daniel Dias" },
                    { 9, "Tania@ipt.pt", "foto.png", "Tânia Gomes" },
                    { 10, "AndreiaG@ipt.pt", "foto.png", "Andreia Correia" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_FicheiroFK",
                table: "Comentarios",
                column: "FicheiroFK");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_QuemComentou",
                table: "Comentarios",
                column: "QuemComentou");

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_FicheiroFK",
                table: "Downloads",
                column: "FicheiroFK");

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_UtilizadorFK",
                table: "Downloads",
                column: "UtilizadorFK");

            migrationBuilder.CreateIndex(
                name: "IX_Ficheiros_AreaFK",
                table: "Ficheiros",
                column: "AreaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Ficheiros_Dono",
                table: "Ficheiros",
                column: "Dono");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Downloads");

            migrationBuilder.DropTable(
                name: "Ficheiros");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
