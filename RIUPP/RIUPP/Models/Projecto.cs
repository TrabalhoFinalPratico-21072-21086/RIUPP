using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Projecto{
        [Key]
        public int id { get; set; }
        public String titulo { get; set; }
        public String Descricao { get; set; }
        public String local { get; set; }
        public int nota { get; set; }

        [ForeignKey(nameof(cadeira))]
        public int cadeiraFK { get; set; }
        public Cadeira cadeira { get; set; }

        [ForeignKey(nameof(desc))]
        public int descFK { get; set; }
        public Descricao desc { get; set; }
    }
}
