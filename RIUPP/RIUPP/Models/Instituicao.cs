using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Instituicao{
        [Key]
        public int id { get; set; }
        public String nome { get; set; }
        public String morada { get; set; }
        public String cidade { get; set; }
        public String distrito { get; set; }
    }
}
