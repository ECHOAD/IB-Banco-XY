using Capo_Datos;
using Contratos.Repository_Contracts;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TransferenciaCreditoRepository : RepositoryBase<TransferenciaCredito, int>, ITransferenciaCreditoRepository
    {
        public TransferenciaCreditoRepository(InternetBanking internetBankingContext) : base(internetBankingContext)
        {
        }
    }
}
