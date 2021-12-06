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
    public class EstadoPrestamoBL : IEstadoPrestamoBL
    {
        public readonly IRepositoryWrapper _repositoryWrapper;


        public EstadoPrestamoBL(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task Delete(EstadoPrestamo entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EstadoPrestamo>> FindByCondition(Expression<Func<EstadoPrestamo, bool>> expresion)
        {
            var lst_row = (await _repositoryWrapper.EstadoPrestamoRepository.FindByCondition(expresion)).Include(x => x.Cuenta);

            return lst_row.ToList();
        }

        public Task<EstadoPrestamo> FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EstadoPrestamo>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Save(EstadoPrestamo cuentaAhorro)
        {
            throw new NotImplementedException();
        }

        public Task Update(EstadoPrestamo entity)
        {
            throw new NotImplementedException();
        }
    }
}
