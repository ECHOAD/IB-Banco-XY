using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contratos.BL_Contracts
{
    public interface IEstadoCuentaBL
    {
        Task<List<EstadoCuenta>> GetAll();

        Task Save(EstadoCuenta cuentaAhorro);

        Task Delete(EstadoCuenta entity);

        Task<EstadoCuenta> FindByID(int id);

        Task<List<EstadoCuenta>> FindByCondition(Expression<Func<EstadoCuenta, bool>> expresion);

        Task Update(EstadoCuenta entity);
    }
}
