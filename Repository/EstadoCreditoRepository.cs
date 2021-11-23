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
    public class EstadoCreditoRepository : RepositoryBase<EstadoCredito, int>, IEstadoCreditoRepository
    {
        public EstadoCreditoRepository(InternetBanking internetBankingContext) : base(internetBankingContext)
        {
        }
    }
}
