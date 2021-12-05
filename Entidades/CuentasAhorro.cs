using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CuentasAhorro
    {


        [Key]
        public int Id { get; set; }

        public string Id_Usuario { get; set; }

        public string Codg_Cuenta { get; set; }

        public double Balance_Actual { get; set; }


        [ForeignKey("Id_Usuario")]
        public Usuarios Usuario { get; set; }

    }
}
