using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TarjetaCredito
    {
        [Key]
        public int Id { get; set; }

        public string numero_tarjetaCredito { get; set; }

        public string Id_usuario { get; set; }

        public double Balance { get; set; }

        public double Balance_disponible { get; set; }

        [ForeignKey("Id_usuario")]
        public Usuarios usuario { get; set; }

    }
}
