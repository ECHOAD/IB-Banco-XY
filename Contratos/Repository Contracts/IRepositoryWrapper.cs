using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratos.Repository_Contracts
{
    //Repository Provider
    public interface IRepositoryWrapper
    {

        public ITarjetaCreditoRepository TarjetaCreditoRepository { get; }

        public IEstadoPrestamoRepository EstadoPrestamoRepository { get; }

        public IEstadoCuentaRepository EstadoCuentaRepository { get; }

        public IEstadoCreditoRepository EstadoCreditoRepository { get; }

        public ICuentaAhorroRepository CuentaAhorroRepository { get; }

        public IPrestamoRepository PrestamoRepository { get; }

        public ITransferenciaCuentaRepository TransferenciaCuentaRepository { get; }


        //Transactional Actions
        Task Save();
        Task<IDbContextTransaction> BeginTransaction();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();




    }
}
