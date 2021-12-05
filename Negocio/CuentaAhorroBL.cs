using Contratos.Repository_Contracts;
using Contratos.BL_Contracts;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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

            return lst_rows.Include(x => x.Usuario).ToList();

        }

        public async Task<CuentasAhorro> FindByID(int id)
        {
            return await repositoryWrapper.CuentaAhorroRepository.FindById(id);
        }

        public async Task<List<CuentasAhorro>> GetAll()
        {
            return (await repositoryWrapper.CuentaAhorroRepository.GetAll()).ToList();
        }

        public async Task CrearCuenta(CuentasAhorro entity)
        {
            var transaction = await repositoryWrapper.BeginTransaction();

            try
            {
                await repositoryWrapper.CuentaAhorroRepository.Create(entity);
                await repositoryWrapper.Save();
                await repositoryWrapper.CommitTransactionAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new Exception($"No se pudo crear la cuenta de No. {entity.Codg_Cuenta}");
            }
        }

        public async Task Update(CuentasAhorro entity)
        {

            await repositoryWrapper.CuentaAhorroRepository.Update(entity);

        }
    }
}
