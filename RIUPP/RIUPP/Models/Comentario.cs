using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Comentario{

        [Key]
        public int id { get; set; }
        public String comentario { get; set; }
        public String visivel { get; set; }
        public DateTime date { get; set; }
    }
}
