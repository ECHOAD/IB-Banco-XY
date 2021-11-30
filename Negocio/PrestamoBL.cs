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
    public class PrestamoBL : IPrestamoBL
    {
        public readonly IRepositoryWrapper _repositoryWrapper;

        public PrestamoBL(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task Delete(Prestamo prestamo)
        {
            var transaction = await _repositoryWrapper.BeginTransaction();

            await _repositoryWrapper.PrestamoRepository.Delete(prestamo);
            await _repositoryWrapper.Save();

            await transaction.CommitAsync();
        }

        public async Task<List<Prestamo>> FindByCondition(Expression<Func<Prestamo, bool>> expresion)
        {
            return (await _repositoryWrapper.PrestamoRepository.FindByCondition(expresion)).ToList();
        }

        public async Task<Prestamo> FindById(int id)
        {    
            return await _repositoryWrapper.PrestamoRepository.FindById(id);
        }

        public async Task<List<Prestamo>> GetAll()
        {
            return (await _repositoryWrapper.PrestamoRepository.GetAll()).ToList();
        }

        public async Task Save(Prestamo prestamo)
        {
            var transaction= await _repositoryWrapper.BeginTransaction();

            await _repositoryWrapper.PrestamoRepository.Create(prestamo);
            await _repositoryWrapper.Save();
            
            await transaction.CommitAsync();
        }

        public async Task Update(Prestamo prestamo)
        {
            var transaction = await _repositoryWrapper.BeginTransaction();

            await _repositoryWrapper.PrestamoRepository.Update(prestamo);
            await _repositoryWrapper.Save();

            await transaction.CommitAsync();
        }
    }
}
