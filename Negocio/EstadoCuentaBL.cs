using Contratos.BL_Contracts;
using Contratos.Repository_Contracts;
using Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EstadoCuentaBL : IEstadoCuentaBL
    {
        private readonly IRepositoryWrapper repositoryWrapper;

        public EstadoCuentaBL(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        public async Task Delete(EstadoCuenta entity)
        {
            var transaction = await repositoryWrapper.BeginTransaction();

            await repositoryWrapper.EstadoCuentaRepository.Delete(entity);
            await repositoryWrapper.Save();

            await transaction.CommitAsync();
        }

        public async Task<List<EstadoCuenta>> FindByCondition(Expression<Func<EstadoCuenta, bool>> expresion)
        {
            var lst_rows = await repositoryWrapper.EstadoCuentaRepository.FindByCondition(expresion);

            return lst_rows.Include(x => x.Cuenta).ThenInclude(x => x.Usuario).ToList();
        }

        public async Task<EstadoCuenta> FindByID(int id)
        {
            return await repositoryWrapper.EstadoCuentaRepository.FindById(id);
        }

        public async Task<List<EstadoCuenta>> GetAll()
        {
            return (await repositoryWrapper.EstadoCuentaRepository.GetAll()).ToList();
        }

        public async Task Save(EstadoCuenta entity)
        {
            await repositoryWrapper.EstadoCuentaRepository.Create(entity);
        }

        public async Task Update(EstadoCuenta entity)
        {

            await repositoryWrapper.EstadoCuentaRepository.Update(entity);
        }
    }
}
