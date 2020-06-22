using Microsoft.EntityFrameworkCore;
using RIUPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Data
{
    public class RIUPPDB : DbContext{
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Ficheiro> Ficheiros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public RIUPPDB(DbContextOptions<RIUPPDB> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Utilizador>().HasData(
               new Utilizador { Id = 1, Nome = "Luís Freitas", Email = "Luis@ipt.pt", Foto = "foto.png" },
               new Utilizador { Id = 2, Nome = "Andreia Gomes", Email = "Andreia@ipt.pt", Foto = "foto.png" },
               new Utilizador { Id = 3, Nome = "Cristina Sousa", Email = "Cristina@ipt.pt", Foto = "foto.png" },
               new Utilizador { Id = 4, Nome = "Sónia Rosa", Email = "Sonia@ipt.pt", Foto = "foto.png" },
               new Utilizador { Id = 5, Nome = "António Santos", Email = "Antonio@ipt.pt", Foto = "foto.png" },
               new Utilizador { Id = 6, Nome = "Gustavo Alves", Email = "Gustavo@ipt.pt", Foto = "foto.png" },
               new Utilizador { Id = 7, Nome = "Rosa Vieira", Email = "Rosa@ipt.pt", Foto = "foto.png" },
               new Utilizador { Id = 8, Nome = "Daniel Dias", Email = "Daniel@ipt.pt", Foto = "foto.png" },
               new Utilizador { Id = 9, Nome = "Tânia Gomes", Email = "Tania@ipt.pt", Foto = "foto.png" },
               new Utilizador { Id = 10, Nome = "Andreia Correia", Email = "AndreiaG@ipt.pt", Foto = "foto.png" }
            );

            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Nome = "Engenharia", Designacao = "" },
                new Area { Id = 2, Nome = "Artes", Designacao = "" },
                new Area { Id = 3, Nome = "Direito", Designacao = "" },
                new Area { Id = 4, Nome = "Economia", Designacao = "" },
                new Area { Id = 5, Nome = "Gestao", Designacao = "" },
                new Area { Id = 6, Nome = "Design", Designacao = "" }
            );

            modelBuilder.Entity<Ficheiro>().HasData(
               new Ficheiro { Id = 1, Titulo = "Documento1", Descricao = "Pequena Descrição", Observacao = "nenhuma", Local = "", Tipo = "", DateUpload = new DateTime(2020, 2, 2), Dono = 4, AreaFK = 1},
               new Ficheiro { Id = 2, Titulo = "Documento2", Descricao = "Pequena Descrição", Observacao = "nenhuma", Local = "", Tipo = "", DateUpload = new DateTime(2020, 2, 2), Dono = 7, AreaFK = 2},
               new Ficheiro { Id = 3, Titulo = "Documento3", Descricao = "Pequena Descrição", Observacao = "nenhuma", Local = "", Tipo = "", DateUpload = new DateTime(2020, 2, 2), Dono = 8, AreaFK = 3}
            );

            modelBuilder.Entity<Comentario>().HasData(
               new Comentario { Id = 1, Coment = "primeiroComentario", Visivel = "S", Date = new DateTime(2020, 6, 6), QuemComentou = 1, FicheiroFK = 1},
               new Comentario { Id = 2, Coment = "segundoComentario", Visivel = "S", Date = new DateTime(2020, 6, 6), QuemComentou = 2, FicheiroFK = 2 },
               new Comentario { Id = 3, Coment = "terceiroComentario", Visivel = "S", Date = new DateTime(2020, 6, 6), QuemComentou = 3, FicheiroFK = 3 },
               new Comentario { Id = 4, Coment = "quartoComentario", Visivel = "S", Date = new DateTime(2020, 6, 6), QuemComentou = 5, FicheiroFK = 1 },
               new Comentario { Id = 5, Coment = "quintoComentario", Visivel = "S", Date = new DateTime(2020, 6, 6), QuemComentou = 6, FicheiroFK = 2 }
            );

            modelBuilder.Entity<Download>().HasData(
               new Download { Id = 1, Data = new DateTime(2020, 6, 6), FicheiroFK = 1, UtilizadorFK = 1},
               new Download { Id = 2, Data = new DateTime(2020, 6, 6), FicheiroFK = 2, UtilizadorFK = 2},
               new Download { Id = 3, Data = new DateTime(2020, 6, 6), FicheiroFK = 2, UtilizadorFK = 3},
               new Download { Id = 4, Data = new DateTime(2020, 6, 6), FicheiroFK = 1, UtilizadorFK = 4},
               new Download { Id = 5, Data = new DateTime(2020, 6, 6), FicheiroFK = 3, UtilizadorFK = 3}
            );
        }
    }
}
