using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Download{
        [Key]
        public int id { get; set; }
        public DateTime data { get; set; }

        [ForeignKey(nameof(ficheiro))]
        public int ficheiroFK { get; set; }
        public Ficheiro ficheiro { get; set; }

        [ForeignKey(nameof(utilizador))]
        public int utilizadorFK { get; set; }
        public Utilizador utilizador { get; set; }

    }
}
