using Capo_Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CuentasAhorroRepository : RepositoryBase<CuentasAhorro, int>
    {
        public CuentasAhorroRepository(InternetBanking internetBankingContext) : base(internetBankingContext)
        {
        }
    }
}
