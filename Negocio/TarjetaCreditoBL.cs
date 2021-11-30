using Contratos.Repository_Contracts;
using Contratos.BL_Contracts;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TarjetaCreditoBL : ITarjetaCreditoBL
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public TarjetaCreditoBL(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task Delete(TarjetaCredito TarjetaCredito)
        {
            var transaction = await _repositoryWrapper.BeginTransaction();

            await _repositoryWrapper.TarjetaCreditoRepository.Delete(TarjetaCredito);
            await _repositoryWrapper.Save();

            await transaction.CommitAsync();
        }

        public async Task<List<TarjetaCredito>> FindByCondition(Expression<Func<TarjetaCredito, bool>> expresion)
        {
            return (await _repositoryWrapper.TarjetaCreditoRepository.FindByCondition(expresion)).ToList();
        }

        public async Task<TarjetaCredito> FindById(int id)
        {
            return await _repositoryWrapper.TarjetaCreditoRepository.FindById(id);
        }

        public async Task<List<TarjetaCredito>> GetAll()
        {
            return (await _repositoryWrapper.TarjetaCreditoRepository.GetAll()).ToList();
        }

        public async Task Save(TarjetaCredito TarjetaCredito)
        {
            var transaction = await _repositoryWrapper.BeginTransaction();

            await _repositoryWrapper.TarjetaCreditoRepository.Create(TarjetaCredito);
            await _repositoryWrapper.Save();

            await transaction.CommitAsync();
        }

        public async Task Update(TarjetaCredito TarjetaCredito)
        {
            var transaction = await _repositoryWrapper.BeginTransaction();

            await _repositoryWrapper.TarjetaCreditoRepository.Update(TarjetaCredito);
            await _repositoryWrapper.Save();

            await transaction.CommitAsync();
        }
    }
}
