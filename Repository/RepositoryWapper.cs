using Capo_Datos;
using Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWapper : IRepositoryWrapper
    {
        private InternetBanking _internetBankingContext;

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

        public IEstadoPrestamoRepository EstadoCreditoRepository
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
                if (_estadoCreditoRepository == null)
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


        public async Task Save()
        {
            try
            {
                await BeginTransaction();

                await _internetBankingContext.SaveChangesAsync();

                await CommitTransactionAsync();

            }
            catch (Exception)
            {
                await RollbackTransactionAsync();

            }
        }


        private async Task BeginTransaction()
        {
            _internetBankingContext.Database.BeginTransaction();
            await Task.CompletedTask;
        }

        private async Task CommitTransactionAsync()
        {
            await _internetBankingContext.Database.CommitTransactionAsync();
        }

        private async Task RollbackTransactionAsync()
        {
            await _internetBankingContext.Database.RollbackTransactionAsync();
        }
    }
}
