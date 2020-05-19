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

        public RIUPPDB(DbContextOptions<RIUPPDB> options) : base(options) { }
    }
}
