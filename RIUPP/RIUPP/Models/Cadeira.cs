using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Cadeira{
        [Key]
        public int id { get; set; }
        public String nome { get; set; }

        [ForeignKey(nameof(curso))]
        public int cursoFK { get; set; }
        public Curso curso { get; set; }
    }
}
