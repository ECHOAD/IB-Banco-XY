using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contratos.BL_Contracts
{
    public interface ICuentaAhorroBL
    {
        Task<List<CuentasAhorro>> GetAll();

        Task Save(CuentasAhorro entity);

        Task Delete(CuentasAhorro entity);

        Task<CuentasAhorro> FindByID(int id);

        Task<List<CuentasAhorro>> FindByCondition(Expression<Func<CuentasAhorro, bool>> expresion);

        Task Update(CuentasAhorro entity);
    }
}
