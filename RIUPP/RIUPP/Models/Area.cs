using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Area{
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Nome da área em questão
        /// </summary>
        [Required] // Para obrigar a ser inserido o atributo
        public String Nome { get; set; } 
        [Required] // Para obrigar a ser inserido o atributo
        public String Designacao { get; set; } // Pequena frase sobre a Área, para a descrever

        // Lista que contém todos os ficheiros associados a cada Área
        public virtual ICollection<Ficheiro> Ficheiro { get; set; }
        public Area(){
            Ficheiro = new HashSet<Ficheiro>();
        }
    }
}
