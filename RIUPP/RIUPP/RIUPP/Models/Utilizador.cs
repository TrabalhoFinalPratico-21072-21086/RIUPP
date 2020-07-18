using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Utilizador{
        [Key]
        public int Id { get; set; }
        [Required] // Para obrigar a ser inserido o atributo
        public String Nome { get; set; }//Nome do utilizador
        public String Email { get; set; }//Email do utilizador
        [Required] // Para obrigar a ser inserido o atributo
        public String Aut { get; set; }//Chave de autentiticação

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
