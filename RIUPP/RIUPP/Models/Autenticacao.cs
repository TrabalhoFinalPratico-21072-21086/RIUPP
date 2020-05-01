using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RIUPP.Models
{
    public class Autenticacao
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
