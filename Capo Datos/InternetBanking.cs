using Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Capo_Datos
{
    public class InternetBanking : IdentityDbContext
    {
        public InternetBanking(DbContextOptions<InternetBanking> options) : base(options)
        {

        }
        public virtual DbSet<EstadoCuenta> EstadoCuentas { get; set; }
        public virtual DbSet<CuentasAhorro> CuentasAhorro { get; set; }
        public virtual DbSet<TarjetaCredito> TarjetasCredito { get; set; }
        public virtual DbSet<EstadoCredito> EstadosCredito { get; set; }
        public virtual DbSet<Prestamo> Prestamos{ get; set; }
        public virtual DbSet<EstadoPrestamo> EstadosPrestamo { get; set; }
        public virtual DbSet<TransferenciaCuenta> Transferencias { get; set; }


    }
}
