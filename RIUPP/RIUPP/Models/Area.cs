using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Area{

        /// <summary>
        /// Id da Área em questão (PK).
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome da Área em questão (obrigatório).
        /// </summary>
        [Required]
        public String Nome { get; set; }

        /// <summary>
        /// Pequena frase sobre a Área, para a descrever. (obrigatório).
        /// </summary>
        [Required]
        public String Designacao { get; set; }



        // Lista/Collection que contém todos os ficheiros associados a cada Área.
        public virtual ICollection<Ficheiro> Ficheiro { get; set; }

        // Construtor
        public Area(){
            Ficheiro = new HashSet<Ficheiro>();
        }
    }
}
