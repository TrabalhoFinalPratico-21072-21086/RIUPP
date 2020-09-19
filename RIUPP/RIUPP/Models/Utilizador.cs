using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Utilizador{

        /// <summary>
        /// Id do respetivo Utilizador (PK).
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador(Obrigatório).
        /// </summary>
        [Required]
        public String Nome { get; set; }

        /// <summary>
        /// Email do utilizador.
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// Chave de autentiticação do utilizador(Obrigatório).
        /// </summary>
        [Required]
        public String Aut { get; set; }

        /// <summary>
        /// Para saber se a conta do respectivo utilizador esta suspensa ou nao
        /// </summary>
        [Required]
        public Boolean Suspenso { get; set; }

        // Listas com os Downloads, Ficheiros e Comentários associados a cada Utilizador
        public virtual ICollection<Download> Download { get; set; }
        public virtual ICollection<Ficheiro> Ficheiro { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }

        // Construtor
        public Utilizador(){
            Download = new HashSet<Download>();
            Ficheiro = new HashSet<Ficheiro>();
            Comentario = new HashSet<Comentario>();
        }

    }
}
