using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Comentario{

        /// <summary>
        /// Id do respetivo comentário (PK).
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Comentário obrigatório, em formato string, com tamanho máximo de 100caracteres.
        /// </summary>
        [Required]
        [StringLength(100)]
        public String Coment { get; set; }

        /// <summary>
        /// Data do comentário obrigatória para saber quando o comentário foi feito.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }


        
        [Required]
        [ForeignKey(nameof(Utilizador))]
        
        /// <summary>
        /// Utilizador que fez o comentário em questão (FK para Utilizador).
        /// </summary>
        public int QuemComentou { get; set; }
        public Utilizador Utilizador { get; set; }

        /// <summary>
        /// Para possibilitar o dono do projecto a esconder comentários.
        /// </summary>
        public Boolean Visivel { get; set; }
        
        /// <summary>
        /// O ficheiro onde foi feito o comentário (obrigatório) (FK para Ficheiro).
        /// </summary>
        [Required]
        [ForeignKey(nameof(Ficheiro))]
        public int FicheiroFK { get; set; }
        public Ficheiro Ficheiro { get; set; }
        
    }
}
