using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Ficheiro{

        /// <summary>
        /// Id do respetivo ficheiro (PK).
        /// </summary>
        [Key]
        public int Id { get; set; }


        /// <summary>
        /// Titulo do ficheiro com limite de 50 caracteres (obrigatório).
        /// </summary>
        [Required]
        [StringLength(50)]
        public String Titulo { get; set; }

        /// <summary>
        /// Descrição do ficheiro (obrigatório).
        /// </summary>
        [Required]
        public String Descricao { get; set; }

        /// <summary>
        /// Observações do ficheiro.
        /// </summary>
        public String Observacao { get; set; }

        /// <summary>
        /// Local onde o ficheiro foi guardado, a sua directoria.
        /// </summary>
        public String Local { get; set; }

        /// <summary>
        /// Data em que foi feito o upload (obrigatório).
        /// </summary>
        [Required]
        public DateTime DateUpload { get; set; }


        /// <summary>
        /// Quem fez o upload do projecto (obrigatório) (FK para Utilizador).
        /// </summary>
        [Required]
        [ForeignKey(nameof(Utilizador))]
        public int Dono { get; set; }
        public Utilizador Utilizador { get; set; }


        /// <summary>
        /// A que Área pertence o projecto (obrigatório) (FK para Area).
        /// </summary>
        [Required]
        [ForeignKey(nameof(Area))]
        public int AreaFK { get; set; }
        public Area Area { get; set; } 


        /// <summary>
        /// Listas que contêm todos os download e comentários associados ao ficheiro em questão
        /// </summary>
        public virtual ICollection<Download> Download { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
        // Construtor
        public Ficheiro(){
            Download = new HashSet<Download>();
            Comentario = new HashSet<Comentario>();
        }

    }
}
