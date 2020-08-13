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
        [Required] // Para obrigar a ser inserido o atributo
        [StringLength(100)]//Limitar o tamanho máximo do Comentário
        public String Coment { get; set; } //Conteudo do comentário
        [Required] // Para obrigar a ser inserido o atributo
        public DateTime Date { get; set; } //Quando foi feito o comentário

        [Required] // Para obrigar a ser inserido o atributo
        [ForeignKey(nameof(Utilizador))]
        public int QuemComentou { get; set; } //O utilizador que fez o comentário
        public Utilizador Utilizador { get; set; }

        public Boolean Visivel { get; set; }//caso o comentario seja improprio, o dono do projecto pode esconde-lo

        [Required] // Para obrigar a ser inserido o atributo
        [ForeignKey(nameof(Ficheiro))]
        public int FicheiroFK { get; set; } //O ficheiro onde foi feito o comentário
        public Ficheiro Ficheiro { get; set; }
        
    }
}
