using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Mensagem{
        [Key]
        public int id { get; set; }
        public String conteudo { get; set; }
        public DateTime dataEnviada { get; set; }
        public DateTime dateRecebida { get; set; }
    }
}
