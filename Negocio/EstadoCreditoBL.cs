using Contratos.BL_Contracts;
using Contratos.Repository_Contracts;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EstadoCreditoBL : IEstadoCreditoBL
    {

        public readonly IRepositoryWrapper _repositoryWrapper;

        public EstadoCreditoBL(IRepositoryWrapper respoitoryWrapper)
        {
            _repositoryWrapper = respoitoryWrapper;
        }


        public Task Delete(EstadoCredito entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EstadoCredito>> FindByCondition(Expression<Func<EstadoCredito, bool>> expresion)
        {
            var lst_row = (await _repositoryWrapper.EstadoCreditoRepository.FindByCondition(expresion));

            return lst_row.Include(x => x.Tarjeta).Include(x => x.CuentaOrigen)
                .ToList();
        }

        public Task<EstadoCredito> FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EstadoCredito>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Save(EstadoCredito cuentaAhorro)
        {
            throw new NotImplementedException();
        }

        public Task Update(EstadoCredito entity)
        {
            throw new NotImplementedException();
        }
    }
}

