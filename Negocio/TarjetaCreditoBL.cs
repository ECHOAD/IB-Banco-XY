using Contratos.Repository_Contracts;
using Contratos.BL_Contracts;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Negocio.Exceptions;

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

        public async Task CreateCreditCard(TarjetaCredito TarjetaCredito)
        {
            var transaction = await _repositoryWrapper.BeginTransaction();

            try
            {

                TarjetaCredito.Balance_disponible = TarjetaCredito.Balance;


                await _repositoryWrapper.TarjetaCreditoRepository.Create(TarjetaCredito);
                await _repositoryWrapper.Save();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new Exception();
            }
        }

        public async Task PayCreditCard(EstadoCredito estadoCredito)
        {


            var transaction = await _repositoryWrapper.BeginTransaction();
            try
            {

                if (estadoCredito.Pagado <= 0)
                {
                    throw new UnnexpectedValueException("El monto a pagar debe de ser mayor que cero");
                }

                estadoCredito.Fecha = DateTime.Now;

                var AccounOrigin = await _repositoryWrapper.CuentaAhorroRepository.FindById(estadoCredito.Id_CuentaOrigen);

                AccounOrigin.Balance_Actual -= estadoCredito.Pagado;



                if (AccounOrigin.Balance_Actual < 0)
                {
                    throw new NoSufficientAmountException("La cuenta no cuenta con suficiente monto para realizar esta accion");
                }

                await _repositoryWrapper.CuentaAhorroRepository.Update(AccounOrigin);

                var TarjetaCredito = await _repositoryWrapper.TarjetaCreditoRepository.FindById(estadoCredito.Id_TarjetaCredito);

                TarjetaCredito.Balance_disponible += estadoCredito.Pagado;

                await _repositoryWrapper.TarjetaCreditoRepository.Update(TarjetaCredito);

                await _repositoryWrapper.EstadoCuentaRepository.Create(
                    new EstadoCuenta { Cuenta = AccounOrigin, Accion = "Retiro", Descripcion = $"Pago a credito XXXX-XXXX-XXXX " +
                    $"{TarjetaCredito.Numero_tarjetaCredito[^4..]} ", Monto = estadoCredito.Pagado });


                estadoCredito.Disponible = TarjetaCredito.Balance_disponible;

                await _repositoryWrapper.EstadoCreditoRepository.Create(estadoCredito);

                await _repositoryWrapper.Save();

                await transaction.CommitAsync();

            }
            catch (NoSufficientAmountException ex)
            {
                await transaction.RollbackAsync();
                throw ex;

            }
            catch (UnnexpectedValueException ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new Exception($"El pago hacia el credito ha fallado, si el problema persiste contacte al Servicio Tecnico");
            }

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
