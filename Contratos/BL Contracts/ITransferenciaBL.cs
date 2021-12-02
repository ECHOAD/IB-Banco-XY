using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contratos.BL_Contracts
{
    public interface ITransferenciaBL
    {
        Task<List<TransferenciaCuenta>> GetAll();

        Task RealizeTransaction(TransferenciaCuenta cuentaAhorro);

        Task Delete(TransferenciaCuenta entity);

        Task<TransferenciaCuenta> FindByID(int id);

        Task<List<TransferenciaCuenta>> FindByCondition(Expression<Func<TransferenciaCuenta, bool>> expresion);

        Task Update(TransferenciaCuenta entity);
    }
}
