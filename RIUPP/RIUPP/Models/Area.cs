using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Area{
        [Key]
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Designacao { get; set; }

        public virtual ICollection<Ficheiro> Ficheiro { get; set; }
        public Area(){
            Ficheiro = new HashSet<Ficheiro>();
        }
    }
}
