using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contratos.BL_Contracts
{
    public interface IEstadoPrestamoBL
    {
        Task<List<EstadoPrestamo>> GetAll();

        Task Save(EstadoPrestamo cuentaAhorro);

        Task Delete(EstadoPrestamo entity);

        Task<EstadoPrestamo> FindByID(int id);

        Task<List<EstadoPrestamo>> FindByCondition(Expression<Func<EstadoPrestamo, bool>> expresion);

        Task Update(EstadoPrestamo entity);
    }
}
