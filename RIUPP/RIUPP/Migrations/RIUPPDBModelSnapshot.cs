﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RIUPP.Data;

namespace RIUPP.Migrations
{
    [DbContext(typeof(RIUPPDB))]
    partial class RIUPPDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RIUPP.Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Designacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Designacao = "",
                            Nome = "Engenharia"
                        },
                        new
                        {
                            Id = 2,
                            Designacao = "",
                            Nome = "Artes"
                        },
                        new
                        {
                            Id = 3,
                            Designacao = "",
                            Nome = "Direito"
                        },
                        new
                        {
                            Id = 4,
                            Designacao = "",
                            Nome = "Economia"
                        },
                        new
                        {
                            Id = 5,
                            Designacao = "",
                            Nome = "Gestao"
                        },
                        new
                        {
                            Id = 6,
                            Designacao = "",
                            Nome = "Design"
                        });
                });

            modelBuilder.Entity("RIUPP.Models.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Coment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("FicheiroFK")
                        .HasColumnType("int");

                    b.Property<int>("QuemComentou")
                        .HasColumnType("int");

                    b.Property<string>("Visivel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FicheiroFK");

                    b.HasIndex("QuemComentou");

                    b.ToTable("Comentarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Coment = "primeiroComentario",
                            Date = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 1,
                            QuemComentou = 1,
                            Visivel = "S"
                        },
                        new
                        {
                            Id = 2,
                            Coment = "segundoComentario",
                            Date = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 2,
                            QuemComentou = 2,
                            Visivel = "S"
                        },
                        new
                        {
                            Id = 3,
                            Coment = "terceiroComentario",
                            Date = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 3,
                            QuemComentou = 3,
                            Visivel = "S"
                        },
                        new
                        {
                            Id = 4,
                            Coment = "quartoComentario",
                            Date = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 1,
                            QuemComentou = 5,
                            Visivel = "S"
                        },
                        new
                        {
                            Id = 5,
                            Coment = "quintoComentario",
                            Date = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 2,
                            QuemComentou = 6,
                            Visivel = "S"
                        });
                });

            modelBuilder.Entity("RIUPP.Models.Download", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("FicheiroFK")
                        .HasColumnType("int");

                    b.Property<int>("UtilizadorFK")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FicheiroFK");

                    b.HasIndex("UtilizadorFK");

                    b.ToTable("Downloads");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Data = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 1,
                            UtilizadorFK = 1
                        },
                        new
                        {
                            Id = 2,
                            Data = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 2,
                            UtilizadorFK = 2
                        },
                        new
                        {
                            Id = 3,
                            Data = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 2,
                            UtilizadorFK = 3
                        },
                        new
                        {
                            Id = 4,
                            Data = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 1,
                            UtilizadorFK = 4
                        },
                        new
                        {
                            Id = 5,
                            Data = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FicheiroFK = 3,
                            UtilizadorFK = 3
                        });
                });

            modelBuilder.Entity("RIUPP.Models.Ficheiro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaFK")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateUpload")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dono")
                        .HasColumnType("int");

                    b.Property<string>("Local")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaFK");

                    b.HasIndex("Dono");

                    b.ToTable("Ficheiros");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AreaFK = 1,
                            DateUpload = new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Pequena Descrição",
                            Dono = 4,
                            Local = "",
                            Observacao = "nenhuma",
                            Tipo = "",
                            Titulo = "Documento1"
                        },
                        new
                        {
                            Id = 2,
                            AreaFK = 2,
                            DateUpload = new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Pequena Descrição",
                            Dono = 7,
                            Local = "",
                            Observacao = "nenhuma",
                            Tipo = "",
                            Titulo = "Documento2"
                        },
                        new
                        {
                            Id = 3,
                            AreaFK = 3,
                            DateUpload = new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Pequena Descrição",
                            Dono = 8,
                            Local = "",
                            Observacao = "nenhuma",
                            Tipo = "",
                            Titulo = "Documento3"
                        });
                });

            modelBuilder.Entity("RIUPP.Models.Utilizador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Utilizadores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Luis@ipt.pt",
                            Foto = "foto.png",
                            Nome = "Luís Freitas"
                        },
                        new
                        {
                            Id = 2,
                            Email = "Andreia@ipt.pt",
                            Foto = "foto.png",
                            Nome = "Andreia Gomes"
                        },
                        new
                        {
                            Id = 3,
                            Email = "Cristina@ipt.pt",
                            Foto = "foto.png",
                            Nome = "Cristina Sousa"
                        },
                        new
                        {
                            Id = 4,
                            Email = "Sonia@ipt.pt",
                            Foto = "foto.png",
                            Nome = "Sónia Rosa"
                        },
                        new
                        {
                            Id = 5,
                            Email = "Antonio@ipt.pt",
                            Foto = "foto.png",
                            Nome = "António Santos"
                        },
                        new
                        {
                            Id = 6,
                            Email = "Gustavo@ipt.pt",
                            Foto = "foto.png",
                            Nome = "Gustavo Alves"
                        },
                        new
                        {
                            Id = 7,
                            Email = "Rosa@ipt.pt",
                            Foto = "foto.png",
                            Nome = "Rosa Vieira"
                        },
                        new
                        {
                            Id = 8,
                            Email = "Daniel@ipt.pt",
                            Foto = "foto.png",
                            Nome = "Daniel Dias"
                        },
                        new
                        {
                            Id = 9,
                            Email = "Tania@ipt.pt",
                            Foto = "foto.png",
                            Nome = "Tânia Gomes"
                        },
                        new
                        {
                            Id = 10,
                            Email = "AndreiaG@ipt.pt",
                            Foto = "foto.png",
                            Nome = "Andreia Correia"
                        });
                });

            modelBuilder.Entity("RIUPP.Models.Comentario", b =>
                {
                    b.HasOne("RIUPP.Models.Ficheiro", "Ficheiro")
                        .WithMany("Comentario")
                        .HasForeignKey("FicheiroFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RIUPP.Models.Utilizador", "Utilizador")
                        .WithMany()
                        .HasForeignKey("QuemComentou")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RIUPP.Models.Download", b =>
                {
                    b.HasOne("RIUPP.Models.Ficheiro", "Ficheiro")
                        .WithMany("Download")
                        .HasForeignKey("FicheiroFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RIUPP.Models.Utilizador", "Utilizador")
                        .WithMany("Download")
                        .HasForeignKey("UtilizadorFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RIUPP.Models.Ficheiro", b =>
                {
                    b.HasOne("RIUPP.Models.Area", "Area")
                        .WithMany("Ficheiro")
                        .HasForeignKey("AreaFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RIUPP.Models.Utilizador", "Utilizador")
                        .WithMany("Ficheiro")
                        .HasForeignKey("Dono")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
