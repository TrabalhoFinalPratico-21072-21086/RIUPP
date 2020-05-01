using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Utilizador{
        [Key][ForeignKey(nameof(autenticacao))]
        public int autenticacaoFK { get; set; }
        public Autenticacao autenticacao { get; set; }
        public String userName { get; set; }
        public String foto { get; set; }
        

    }
}
