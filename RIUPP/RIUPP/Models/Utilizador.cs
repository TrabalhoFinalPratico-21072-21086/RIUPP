using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Utilizador{
        [Key]
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String Foto { get; set; }

        public virtual ICollection<Download> Download { get; set; }
        public virtual ICollection<Ficheiro> Ficheiro { get; set; }

        public Utilizador(){
            Download = new HashSet<Download>();
            Ficheiro = new HashSet<Ficheiro>();
        }

    }
}
