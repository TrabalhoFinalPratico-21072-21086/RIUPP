using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Up{
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(reg))]
        public int regId { get; set; }
        public Registado reg { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(proj))]
        public int projId { get; set; }
        public Projecto proj { get; set; }
    }
}
