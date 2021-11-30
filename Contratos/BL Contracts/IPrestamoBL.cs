using Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contratos.BL_Contracts
{
    public interface IPrestamoBL
    {
        Task<List<Prestamo>> GetAll();

        Task Save(Prestamo prestamo);

        Task Delete(Prestamo prestamo);

        Task<Prestamo> FindById(int id);

        Task<List<Prestamo>> FindByCondition(Expression<Func<Prestamo, bool>> expresion);

        Task Update(Prestamo prestamo);


    }
}
