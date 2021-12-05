using Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contratos.BL_Contracts
{
    public interface ITarjetaCreditoBL
    {
        Task<List<TarjetaCredito>> GetAll();

        Task CreateCreditCard(TarjetaCredito TarjetaCredito);

        Task PayCreditCard(EstadoCredito EstadoCredito);


        Task Delete(TarjetaCredito TarjetaCredito);

        Task<TarjetaCredito> FindById(int id);

        Task<List<TarjetaCredito>> FindByCondition(Expression<Func<TarjetaCredito, bool>> expresion);

        Task Update(TarjetaCredito TarjetaCredito);

    }
}
