using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contratos
{
    public interface IRepository<T, E>
    {

        public Task Create(T entity);

        public Task Delete(T entity);

        public Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expresion);

        public Task<T> FindById(E id);

        public Task<IQueryable<T>> GetAll();

        public Task Update(T entity);
    }
}
