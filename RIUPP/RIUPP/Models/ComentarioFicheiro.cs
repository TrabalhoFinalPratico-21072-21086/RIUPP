using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models
{
    public class ComentarioFicheiro
    {
        [Key][ForeignKey(nameof(comentario))]
        public int comentarioFK { get; set; }
        public Comentario comentario { get; set; }

        [ForeignKey(nameof(ficheiro))]
        public int ficheiroFK { get; set; }
        public Ficheiro ficheiro { get; set; }
    }
}
