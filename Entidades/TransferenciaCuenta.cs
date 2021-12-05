using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TransferenciaCuenta
    {
        [Key]
        public int Id { get; set; }

        public int Id_CuentaOrigen { get; set; }


        public int Id_CuentaDestino { get; set; }

        public double MontoActual { get; set; }

        [ForeignKey("Id_CuentaOrigen")]
        public CuentasAhorro CuentaOrigen { get; set; }

        [ForeignKey("Id_CuentaDestino")]
        public CuentasAhorro CuentaDestino { get; set; }

        public DateTime FechaRealizada { get; set; }

    }
}
