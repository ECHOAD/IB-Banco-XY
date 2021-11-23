using Capo_Datos;
using Contratos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EstadoCuentaRepository : RepositoryBase<EstadoCuenta, int>, IEstadoCuentaRepository
    {
        public EstadoCuentaRepository(InternetBanking internetBankingContext) : base(internetBankingContext)
        {
        }
    }
}
