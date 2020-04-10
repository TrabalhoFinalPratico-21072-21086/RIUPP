using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Registado{
        [Key][ForeignKey(nameof(util))]
        public int id { get; set; }
        public Utilizador util { get; set; }


        public String userName { get; set; }
        public String primeiroNome { get; set; }
        public String ultimoNome { get; set; }
        public String password { get; set; }
        public String descricao { get; set; }
        public String privilegio { get; set;}
    }
}
