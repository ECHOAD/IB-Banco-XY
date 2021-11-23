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
    public class EstadoCuenta
    {

        public EstadoCuenta()
        {
            this.Fecha = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public int Id_cuenta { get; set; }


        public string Accion { get; set; }

        public double Monto { get; set; }

        [DefaultValue("GETDATE()")]
        public DateTime? Fecha { get; set; }


        [ForeignKey("Id_cuenta")]
        public CuentasAhorro cuenta { get; set; }


    }
}
