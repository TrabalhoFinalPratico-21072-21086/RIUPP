using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Download{

        /// <summary>
        /// Id do download (PK).
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Data do download (obrigatório).
        /// </summary>
        [Required]
        public DateTime Data { get; set; }


        /// <summary>
        /// Qual o ficheiro do qual foi feito download (obrigatório)(FK para Ficheiro).
        /// </summary>
        [Required]
        [ForeignKey(nameof(Ficheiro))]
        public int FicheiroFK { get; set; }
        public Ficheiro Ficheiro { get; set; }


        /// <summary>
        /// Quem fez o download do ficheiro (obrigatório)(FK para Utilizador).
        /// </summary>
        [Required]
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

    }
}
