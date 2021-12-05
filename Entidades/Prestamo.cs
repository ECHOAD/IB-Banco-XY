using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }

  
        public string Codigo_Prestamo { get; set; }

  
        public double Balance_Apertura { get; set; }

        public double Total_Pagado { get; set; }

     
        public string Id_Usuario { get; set; }

        [ForeignKey("Id_Usuario")]
        public Usuarios Usuarios { get; set; }



    }
}
