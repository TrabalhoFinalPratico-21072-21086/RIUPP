using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Comentario{

        [Key]
        public int Id { get; set; }
        public String Coment { get; set; }
        public String Visivel { get; set; }
        public DateTime Date { get; set; }

        public int UtilizadorFK { get; set; }

        [ForeignKey(nameof(Ficheiro))]
        public int FicheiroFK { get; set; }
        public Ficheiro Ficheiro { get; set; }
        
    }
}
