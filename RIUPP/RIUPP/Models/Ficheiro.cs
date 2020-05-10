using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Ficheiro{
        [Key]
        public int id { get; set; }
        public String titulo { get; set; }
        public String descricao { get; set; }
        public String observacao { get; set; }
        public String local { get; set; }// sitio onde o ficheiro esta guardado(directoria)
        public String tipo { get; set; }//tipo de ficheiro ex: pdf, zip, jpg
        public DateTime dateupload { get; set; }


        [ForeignKey(nameof(dono))]
        public int donoFK { get; set; }
        public Utilizador dono { get; set; }

        [ForeignKey(nameof(area))]
        public int areaFK { get; set; }
        public Utilizador area { get; set; }

        public virtual ICollection<Download> Download { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
        public Ficheiro(){
            Download = new HashSet<Download>();
            Comentario = new HashSet<Comentario>();
        }


    }
}
