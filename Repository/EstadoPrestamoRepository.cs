﻿using Capo_Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EstadoPrestamoRepository : RepositoryBase<EstadoPrestamo, int>
    {
        public EstadoPrestamoRepository(InternetBanking internetBankingContext) : base(internetBankingContext)
        {
        }
    }
}
