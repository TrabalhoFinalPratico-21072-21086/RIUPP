using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Download{
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }

        [ForeignKey(nameof(Ficheiro))]
        public int FicheiroFK { get; set; }
        public Ficheiro Ficheiro { get; set; }

        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

    }
}
