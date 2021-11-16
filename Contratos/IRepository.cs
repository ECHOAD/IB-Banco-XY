using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratos
{
    public interface IRepository<T, E>
    {
        Task saveEntity(T entity);
        Task updateEntity(T entity);
        Task delete(T entity);
        Task<T> findEntity(E id);
        Task<List<E>> getEntity();

    }
}
