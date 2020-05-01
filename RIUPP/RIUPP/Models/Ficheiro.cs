using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Ficheiro{
        [Key]
        public int id { get; set; }
        public String titulo { get; set; }
        public String descricao { get; set; }
        public String observacao { get; set; }
        public String local { get; set; }
        public String tipo { get; set; }
        public DateTime dateupload { get; set; }


        [ForeignKey(nameof(utilizador))]
        public int utilizadorFK { get; set; }
        public Utilizador utilizador { get; set; }

        [ForeignKey(nameof(area))]
        public int areaFK { get; set; }
        public Utilizador area { get; set; }
    }
}
