using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models{
    public class Telemovel{
        [Key][ForeignKey(nameof(reg))]
        public int idTel { get; set; }
        public Registado reg { get; set; }

        public String telemovel { get; set; }
    }
