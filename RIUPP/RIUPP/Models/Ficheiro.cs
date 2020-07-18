using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Ficheiro{
        [Key]
        public int Id { get; set; }
        [Required]// Para obrigar a ser inserido o atributo
        [StringLength(50)] //Limitar a 50 caracteres
        public String Titulo { get; set; }//Nome do ficheiro
        [Required]// Para obrigar a ser inserido o atributo
        public String Descricao { get; set; }//Pequena descriçao do ficheiro
        public String Observacao { get; set; }// Caso o utilizador que carregou este ficheiro queira dizer algo em especial
        public String Local { get; set; }// sitio onde o ficheiro esta guardado(directoria)
        [Required]// Para obrigar a ser inserido o atributo
        public DateTime DateUpload { get; set; }//Data em que foi feito o upload

        [Required]// Para obrigar a ser inserido o atributo
        [ForeignKey(nameof(Utilizador))]
        public int Dono { get; set; } //Quem fez o upload do projecto
        public Utilizador Utilizador { get; set; }

        [Required] // Para obrigar a ser inserido o atributo
        [ForeignKey(nameof(Area))]
        public int AreaFK { get; set; } // A que Área pertence o projecto
        public Area Area { get; set; } 

        // Listas que contêm todos os download e comentários associados ao ficheiro em questão
        public virtual ICollection<Download> Download { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
        // Construtor
        public Ficheiro(){
            Download = new HashSet<Download>();
            Comentario = new HashSet<Comentario>();
        }

    }
}
