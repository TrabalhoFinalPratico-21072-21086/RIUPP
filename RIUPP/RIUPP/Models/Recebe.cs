using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Recebe{
        [ForeignKey(nameof(mens))]
        public int idMens { get; set; }
        public Mensagem mens { get; set; }

        [Key][ForeignKey(nameof(reg))]
        public int idReg { get; set; }
        public Registado reg { get; set; }
    }
}
