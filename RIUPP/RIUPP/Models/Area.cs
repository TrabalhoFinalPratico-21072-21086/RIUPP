using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Area{
        [Key]
        public int id { get; set; }
        public String nome { get; set; }
        public String designacao { get; set; }

        public virtual ICollection<Ficheiro> Ficheiro { get; set; }
        public Area(){
            Ficheiro = new HashSet<Ficheiro>();
        }
    }
}
