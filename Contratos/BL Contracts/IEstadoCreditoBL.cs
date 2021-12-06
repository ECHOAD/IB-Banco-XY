using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contratos.BL_Contracts
{
    public interface IEstadoCreditoBL
    {
        Task<List<EstadoCredito>> GetAll();

        Task Save(EstadoCredito cuentaAhorro);

        Task Delete(EstadoCredito entity);

        Task<EstadoCredito> FindByID(int id);

        Task<List<EstadoCredito>> FindByCondition(Expression<Func<EstadoCredito, bool>> expresion);

        Task Update(EstadoCredito entity);
    }
}
