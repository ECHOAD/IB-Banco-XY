using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TransferenciaCredito
    {
        [Key]
        public int Id { get; set; }

        public int Id_CreditoOrigen { get; set; }


        public int Id_CuentaDestino { get; set; }

        public double MontoActual { get; set; }

        [ForeignKey("Id_CreditoOrigen")]
        public TarjetaCredito TarjetaCredito { get; set; }

        [ForeignKey("Id_CuentaDestino")]
        public CuentasAhorro CuentaDestino { get; set; }

        public DateTime FechaRealizada { get; set; }

    }
}
