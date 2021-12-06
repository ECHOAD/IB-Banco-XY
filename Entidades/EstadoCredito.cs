using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EstadoCredito
    {

        public EstadoCredito()
        {
            this.Fecha = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_TarjetaCredito { get; set; }

        [Required]
        public int Id_CuentaOrigen { get; set; }

        public double Pagado { get; set; }

        public double Disponible { get; set; }

        public DateTime? Fecha { get; set; }

        [ForeignKey("Id_TarjetaCredito")]
        public TarjetaCredito Tarjeta { get; set; }

        [ForeignKey("Id_CuentaOrigen")]
        public CuentasAhorro CuentaOrigen { get; set; }
    }
}
