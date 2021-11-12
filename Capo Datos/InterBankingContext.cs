using Entidades;
using Microsoft.EntityFrameworkCore;
using System;

namespace Capo_Datos
{
    public class InterBankingContext:DbContext
    {
        public InterBankingContext(DbContextOptions<InterBankingContext> options) : base(options)
        {

        }

        public virtual DbSet<Usuarios> Usuarios { get; set; }



    }
}
