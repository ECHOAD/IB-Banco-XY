using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuarios
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(30)]
        public string Usuario {  get; set; }

        public string Password { get; set;  }

    }
}
