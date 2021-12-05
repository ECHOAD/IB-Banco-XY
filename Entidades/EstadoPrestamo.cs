using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EstadoPrestamo
    {
        public EstadoPrestamo()
        {
            this.Fecha = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_CuentaOrigen { get; set; }

        [Required]
        public int Id_prestamo { get; set; }

        public double Monto_pagado { get; set; }

        public double Monto_restante { get; set; }

        public DateTime? Fecha { get; set; }

        [ForeignKey("Id_CuentaOrigen")]
        public CuentasAhorro Cuenta { get; set; }

        [ForeignKey("Id_prestamo")]
        public Prestamo Prestamo { get; set; }
    }
}
