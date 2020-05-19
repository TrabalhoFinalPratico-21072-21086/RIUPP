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
        /*[Key][ForeignKey(nameof(autenticacao))]
        public int autenticacaoFK { get; set; }
        public Autenticacao autenticacao { get; set; }*/
        public String UserName { get; set; }
        public String Foto { get; set; }

        public virtual ICollection<Download> Download { get; set; }

        public Utilizador(){
            Download = new HashSet<Download>();
        }

    }
}
