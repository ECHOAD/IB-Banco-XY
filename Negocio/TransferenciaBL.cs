using Contratos.Repository_Contracts;
using Contratos.BL_Contracts;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Negocio.Exceptions;

namespace Negocio
{
    public class TransferenciaBL : ITransferenciaBL
    {
        private readonly IRepositoryWrapper repositoryWrapper;

        private readonly IEstadoCuentaBL estadoCuentaBL;
        private readonly ICuentaAhorroBL cuentaAhorroBL;

        public TransferenciaBL(IRepositoryWrapper repositoryWrapper,
            IEstadoCuentaBL estadoCuentaBL,
            ICuentaAhorroBL cuentaAhorroBL)
        {
            this.repositoryWrapper = repositoryWrapper;
            this.estadoCuentaBL = estadoCuentaBL;
            this.cuentaAhorroBL = cuentaAhorroBL;
        }

        public async Task Delete(TransferenciaCuenta entity)
        {
            var transaction = await repositoryWrapper.BeginTransaction();

            await repositoryWrapper.TransferenciaCuentaRepository.Delete(entity);
            await repositoryWrapper.Save();

            await transaction.CommitAsync();
        }

        public async Task<List<TransferenciaCuenta>> FindByCondition(Expression<Func<TransferenciaCuenta, bool>> expresion)
        {
            var lst_rows = await repositoryWrapper.TransferenciaCuentaRepository.FindByCondition(expresion);
            return lst_rows.Include("CuentasAhorro").ToList();

        }

        public async Task<TransferenciaCuenta> FindByID(int id)
        {
            return await repositoryWrapper.TransferenciaCuentaRepository.FindById(id);
        }

        public async Task<List<TransferenciaCuenta>> GetAll()
        {
            return (await repositoryWrapper.TransferenciaCuentaRepository.GetAll()).ToList();
        }



        public async Task RealizeCreditTransaction(TransferenciaCredito entity)
        {

            var transaction = await repositoryWrapper.BeginTransaction();

            try
            {

                if (entity.MontoActual <= 0)
                    throw new UnnexpectedValueException("El monto ingresado no puede ser menor o igual cero");


                entity.FechaRealizada = DateTime.Now;



                var CreditoOrigen = await repositoryWrapper.TarjetaCreditoRepository.FindById(entity.Id_CreditoOrigen);
                var cuentaDestino = await repositoryWrapper.CuentaAhorroRepository.FindById(entity.Id_CuentaDestino);

                if (CreditoOrigen.Balance_disponible <= 0 || CreditoOrigen.Balance_disponible - entity.MontoActual < 0)
                    throw new NoSufficientAmountException($"La cuenta no tiene suficiente balance para hacer la transferencia");

                CreditoOrigen.Balance_disponible -= entity.MontoActual;
                cuentaDestino.Balance_Actual += entity.MontoActual;

                await repositoryWrapper.TarjetaCreditoRepository.Update(CreditoOrigen);
                await cuentaAhorroBL.Update(cuentaDestino);


                await repositoryWrapper.EstadoCreditoRepository.Create(new EstadoCredito
                {
                    CuentaOrigen = cuentaDestino,
                    Pagado = -entity.MontoActual,
                    Tarjeta = CreditoOrigen,
                    Disponible = CreditoOrigen.Balance_disponible
                });
                await estadoCuentaBL.Save(new EstadoCuenta
                {
                    Accion = "Deposito",
                    Descripcion = $"Transferencia de: No. Tarjeta: xxxxxxxxxxx{CreditoOrigen.Numero_tarjetaCredito[^4..]}",
                    Cuenta = cuentaDestino,
                    Monto = entity.MontoActual,
                    Fecha = entity.FechaRealizada
                });


                await repositoryWrapper.TransferenciaCreditoRepository.Create(entity);

                await repositoryWrapper.Save();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new Exception("No se pudo realizar la transaccion");
            }
        }

        public async Task RealizeTransaction(TransferenciaCuenta entity)
        {
            var transaction = await repositoryWrapper.BeginTransaction();

            try
            {

                if (entity.MontoActual <= 0)
                    throw new UnnexpectedValueException("El monto ingresado no puede ser menor o igual cero");


                entity.FechaRealizada = DateTime.Now;



                var CuentaOrigen = await repositoryWrapper.CuentaAhorroRepository.FindById(entity.Id_CuentaOrigen);
                var cuentaDestino = await repositoryWrapper.CuentaAhorroRepository.FindById(entity.Id_CuentaDestino);

                if (CuentaOrigen.Balance_Actual <= 0)
                    throw new NoSufficientAmountException($"La cuenta no tiene suficiente balance para hacer la transferencia");

                CuentaOrigen.Balance_Actual -= entity.MontoActual;
                cuentaDestino.Balance_Actual += entity.MontoActual;

                await cuentaAhorroBL.Update(CuentaOrigen);
                await cuentaAhorroBL.Update(cuentaDestino);


                await estadoCuentaBL.Save(new EstadoCuenta { Accion = "Retiro", Descripcion = $"Transferencia a : No. Cuenta: {CuentaOrigen.Codg_Cuenta}", Cuenta = CuentaOrigen, Monto = entity.MontoActual, Fecha = entity.FechaRealizada });
                await estadoCuentaBL.Save(new EstadoCuenta { Accion = "Deposito", Descripcion = $"Transferencia de: No. Cuenta:{CuentaOrigen.Codg_Cuenta}", Cuenta = cuentaDestino, Monto = entity.MontoActual, Fecha = entity.FechaRealizada });


                await repositoryWrapper.TransferenciaCuentaRepository.Create(entity);

                await repositoryWrapper.Save();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new Exception("No se pudo realizar la transaccion");
            }

        }

        public async Task Update(TransferenciaCuenta entity)
        {

            var transaction = await repositoryWrapper.BeginTransaction();

            await repositoryWrapper.TransferenciaCuentaRepository.Update(entity);
            await repositoryWrapper.Save();

            await transaction.CommitAsync();
        }
    }
}
