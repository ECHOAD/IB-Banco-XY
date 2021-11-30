using Contratos.Repository_Contracts;
using Contratos.BL_Contracts;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Negocio
{
    public class CuentaAhorroBL : ICuentaAhorroBL
    {
        private readonly IRepositoryWrapper repositoryWrapper;


        public CuentaAhorroBL(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        public async Task Delete(CuentasAhorro entity)
        {
            var transaction = await repositoryWrapper.BeginTransaction();

            await repositoryWrapper.CuentaAhorroRepository.Delete(entity);
            await repositoryWrapper.Save();

            await transaction.CommitAsync();
        }

        public async Task<List<CuentasAhorro>> FindByCondition(Expression<Func<CuentasAhorro, bool>> expresion)
        {
            var lst_rows = await repositoryWrapper.CuentaAhorroRepository.FindByCondition(expresion);

            return lst_rows.ToList();

        }

        public async Task<CuentasAhorro> FindByID(int id)
        {
            return await repositoryWrapper.CuentaAhorroRepository.FindById(id);
        }

        public async Task<List<CuentasAhorro>> GetAll()
        {
            return (await repositoryWrapper.CuentaAhorroRepository.GetAll()).ToList();
        }

        public async Task Save(CuentasAhorro entity)
        {
            var transaction= await repositoryWrapper.BeginTransaction();

            await repositoryWrapper.CuentaAhorroRepository.Create(entity);
            await repositoryWrapper.Save();
            
            await transaction.CommitAsync();

        }

        public async Task Update(CuentasAhorro entity)
        {

            var transaction = await repositoryWrapper.BeginTransaction();

            await repositoryWrapper.CuentaAhorroRepository.Update(entity);
            await repositoryWrapper.Save();

            await transaction.CommitAsync();
        }
    }
}
