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
        public String Titulo { get; set; }
        public String Descricao { get; set; }
        public String Observacao { get; set; }
        public String Local { get; set; }// sitio onde o ficheiro esta guardado(directoria)
        public String Tipo { get; set; }//tipo de ficheiro ex: pdf, zip, jpg
        public DateTime DateUpload { get; set; }


        [ForeignKey(nameof(Dono))]
        public int DonoFK { get; set; }
        public Utilizador Dono { get; set; }

        [ForeignKey(nameof(Area))]
        public int AreaFK { get; set; }
        public Area Area { get; set; }

        public virtual ICollection<Download> Download { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
        public Ficheiro(){
            Download = new HashSet<Download>();
            Comentario = new HashSet<Comentario>();
        }


    }
}
