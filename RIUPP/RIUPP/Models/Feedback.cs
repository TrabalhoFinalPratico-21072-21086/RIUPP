using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Feedback{

        [Key]
        public int id { get; set; }
        public String comentario { get; set; }
        public Boolean gosto { get; set; }
        public Boolean desgosto { get; set; }
        public Boolean reportar { get; set; }

        [ForeignKey(nameof(proj))]
        public int projFK { get; set; }
        public Projecto proj { get; set; }

        [ForeignKey(nameof(reg))]
        public int regFK { get; set; }
        public Registado reg { get; set; }
    }
}
