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
        [Required]// Para obrigar a ser inserido o atributo
        public DateTime Data { get; set; } //Data em que foi feita o download

        [Required]// Para obrigar a ser inserido o atributo
        [ForeignKey(nameof(Ficheiro))]
        public int FicheiroFK { get; set; } //Qual o ficheiro que foi feito download
        public Ficheiro Ficheiro { get; set; }

        [Required]// Para obrigar a ser inserido o atributo
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; } //Quem fez o download do ficheiro
        public Utilizador Utilizador { get; set; }

    }
}
