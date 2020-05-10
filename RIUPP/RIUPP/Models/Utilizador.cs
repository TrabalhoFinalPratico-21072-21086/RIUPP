using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Utilizador{
        [Key]
        public int id { get; set; }
        [Key][ForeignKey(nameof(autenticacao))]
        public int autenticacaoFK { get; set; }
        public Autenticacao autenticacao { get; set; }
        public String userName { get; set; }
        public String foto { get; set; }

        public virtual ICollection<Ficheiro> Ficheiro { get; set; }
        public virtual ICollection<Download> Download { get; set; }
        public virtual ICollection<Comentario> Comentario{ get; set; }

        public Utilizador(){
            Ficheiro = new HashSet<Ficheiro>();
            Download = new HashSet<Download>();
            Comentario = new HashSet<Comentario>();
        }

    }
}
