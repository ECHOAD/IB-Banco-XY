using Capo_Datos;
using Contratos.Repository_Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWapper : IRepositoryWrapper
    {
        private readonly InternetBanking _internetBankingContext;

        public RepositoryWapper(InternetBanking internetBankingContext)
        {
            this._internetBankingContext = internetBankingContext;

        }

        private ITarjetaCreditoRepository _tarjetaCreditoRepository;
        private IEstadoPrestamoRepository _estadoPrestamoRepository;
        private IEstadoCuentaRepository _estadoCuentaRepository;
        private IEstadoCreditoRepository _estadoCreditoRepository;
        private ICuentaAhorroRepository _cuentaRepository;
        private IPrestamoRepository _prestamoRepository;
        private ITransferenciaCuentaRepository _transferenciaCuentaRepository;
        private ITransferenciaCreditoRepository _transferenciaCreditoRepository;

        public ITarjetaCreditoRepository TarjetaCreditoRepository
        {
            get
            {
                if (_tarjetaCreditoRepository == null)
                {
                    _tarjetaCreditoRepository = new TarjetaCreditoRepository(_internetBankingContext);
                }

                return _tarjetaCreditoRepository;
            }
        }

        public IEstadoPrestamoRepository EstadoPrestamoRepository
        {
            get
            {
                if (_estadoPrestamoRepository == null)
                {
                    _estadoPrestamoRepository = new EstadoPrestamoRepository(_internetBankingContext);
                }

                return _estadoPrestamoRepository;
            }
        }

        public IEstadoCuentaRepository EstadoCuentaRepository
        {
            get
            {
                if (_estadoCuentaRepository == null)
                {
                    _estadoCuentaRepository = new EstadoCuentaRepository(_internetBankingContext);
                }

                return _estadoCuentaRepository;
            }
        }

        public ICuentaAhorroRepository CuentaAhorroRepository
        {
            get
            {
                if (_cuentaRepository == null)
                {
                    _cuentaRepository = new CuentasAhorroRepository(_internetBankingContext);
                }
                return _cuentaRepository;

            }
        }


        public IPrestamoRepository PrestamoRepository
        {
            get
            {
                if (_prestamoRepository == null)
                {
                    _prestamoRepository = new PrestamoRepository(_internetBankingContext);
                }
                return _prestamoRepository;
            }
        }

        public IEstadoCreditoRepository EstadoCreditoRepository
        {
            get
            {
                if (_estadoCreditoRepository == null)
                {
                    _estadoCreditoRepository = new EstadoCreditoRepository(_internetBankingContext); ;
                }
                return _estadoCreditoRepository;
            }
        }

        public ITransferenciaCuentaRepository TransferenciaCuentaRepository
        {
            get
            {
                if (_transferenciaCuentaRepository == null)
                {
                    _transferenciaCuentaRepository = new TransferenciaCuentaRepository(_internetBankingContext);
                }
                return _transferenciaCuentaRepository;
            }
        }

        public ITransferenciaCreditoRepository TransferenciaCreditoRepository
        {
            get
            {
                if (_transferenciaCreditoRepository == null)
                {
                    return _transferenciaCreditoRepository = new TransferenciaCreditoRepository(_internetBankingContext);
                }

                return _transferenciaCreditoRepository;
            }
        }

        public async Task Save()
        {
            try
            {
                await _internetBankingContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
            }
        }


        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _internetBankingContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _internetBankingContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _internetBankingContext.Database.RollbackTransactionAsync();
        }



    }
}
