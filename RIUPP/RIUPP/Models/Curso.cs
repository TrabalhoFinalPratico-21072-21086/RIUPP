using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Curso{
        [Key]
        public int id { get; set;}
        public String nome { get; set; }
        public String codCurso { get; set; }

        [ForeignKey(nameof(area))]
        public int areaFK { get; set; }
        public Area area { get; set; }

        [ForeignKey(nameof(inst))]
        public int instFK { get; set; }
        public Instituicao inst { get; set; }

    }
}
