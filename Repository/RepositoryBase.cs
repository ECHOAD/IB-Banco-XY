using Capo_Datos;
using Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T,E> : IRepository<T,E> where T : class 
    {
        public InternetBanking _internetBankingContext;

        public RepositoryBase(InternetBanking internetBankingContext)
        {
            this._internetBankingContext = internetBankingContext;
        }

        public async Task Create(T entity)
        {
            await _internetBankingContext.Set<T>().AddAsync(entity);
            await Task.CompletedTask;
        }

        public async Task Delete(T entity)
        {
            _internetBankingContext.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expresion)
        {
            var lst_rows = _internetBankingContext.Set<T>().Where(expresion).AsNoTracking();

            return await Task.FromResult(lst_rows);

        }

        public async Task<T> FindById(E id)
        {
            var obj_entity= await _internetBankingContext.Set<T>().FindAsync(id);

            return obj_entity;
        }

        public async  Task<IQueryable<T>> GetAll()
        {
            var lst_rows = _internetBankingContext.Set<T>().AsNoTracking();

            return await Task.FromResult(lst_rows);
        }

        public async Task Update(T entity)
        {
            _internetBankingContext.Set<T>().Update(entity);

            await Task.CompletedTask;
        }
    }
}
