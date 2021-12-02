using Contratos.Repository_Contracts;
using Contratos.BL_Contracts;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task RealizeTransaction(TransferenciaCuenta entity)
        {
            entity.FechaRealizada = DateTime.Now;

            var transaction = await repositoryWrapper.BeginTransaction();

            var CuentaOrigen = await repositoryWrapper.CuentaAhorroRepository.FindById(entity.Id_CuentaOrigen);
            var cuentaDestino = await repositoryWrapper.CuentaAhorroRepository.FindById(entity.Id_CuentaDestino);

            CuentaOrigen.Balance_Actual -= entity.MontoActual;
            cuentaDestino.Balance_Actual += entity.MontoActual;

            await cuentaAhorroBL.Update(CuentaOrigen);
            await cuentaAhorroBL.Update(cuentaDestino);


            await estadoCuentaBL.Save(new EstadoCuenta { Accion = "Transferencia", Cuenta = CuentaOrigen, Monto = entity.MontoActual, Fecha = entity.FechaRealizada });
            await estadoCuentaBL.Save(new EstadoCuenta { Accion = "Deposito", Cuenta = cuentaDestino, Monto = entity.MontoActual, Fecha = entity.FechaRealizada });


            await repositoryWrapper.TransferenciaCuentaRepository.Create(entity);

            await repositoryWrapper.Save();
            await transaction.CommitAsync();

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
