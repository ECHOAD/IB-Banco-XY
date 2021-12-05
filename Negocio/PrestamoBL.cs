using Contratos.Repository_Contracts;
using Contratos.BL_Contracts;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Negocio.Exceptions;

namespace Negocio
{
    public class PrestamoBL : IPrestamoBL
    {
        public readonly IRepositoryWrapper _repositoryWrapper;

        public PrestamoBL(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task Delete(Prestamo prestamo)
        {
            var transaction = await _repositoryWrapper.BeginTransaction();

            await _repositoryWrapper.PrestamoRepository.Delete(prestamo);
            await _repositoryWrapper.Save();

            await transaction.CommitAsync();
        }

        public async Task<List<Prestamo>> FindByCondition(Expression<Func<Prestamo, bool>> expresion)
        {
            return (await _repositoryWrapper.PrestamoRepository.FindByCondition(expresion)).ToList();
        }

        public async Task<Prestamo> FindById(int id)
        {
            return await _repositoryWrapper.PrestamoRepository.FindById(id);
        }

        public async Task<List<Prestamo>> GetAll()
        {
            return (await _repositoryWrapper.PrestamoRepository.GetAll()).ToList();
        }

        public async Task CreatePrestamo(Prestamo prestamo)
        {
            var transaction = await _repositoryWrapper.BeginTransaction();
            try
            {
                await _repositoryWrapper.PrestamoRepository.Create(prestamo);
                await _repositoryWrapper.Save();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new Exception("Hubo un problema al manejar la solicitud");
            }
        }

        public async Task PayPrestamo(EstadoPrestamo estadoPrestamo)
        {

            if (estadoPrestamo.Monto_pagado <= 0)
                throw new UnnexpectedValueException("El monto no puede ser menor que cero");

            var transaction = await _repositoryWrapper.BeginTransaction();

            try
            {
                var accountOrigin = await _repositoryWrapper.CuentaAhorroRepository.FindById(estadoPrestamo.Id_CuentaOrigen);
                var Prestamo = await _repositoryWrapper.PrestamoRepository.FindById(estadoPrestamo.Id_prestamo);

                accountOrigin.Balance_Actual -= estadoPrestamo.Monto_pagado;

                if (accountOrigin.Balance_Actual < 0)
                    throw new NoSufficientAmountException($"La cuenta no contiene suficiente balance para pagar dicha cantidad");

                await _repositoryWrapper.CuentaAhorroRepository.Update(accountOrigin);


                await _repositoryWrapper.EstadoCuentaRepository.Create(new EstadoCuenta
                {
                    Cuenta = accountOrigin,
                    Accion = "Retiro",
                    Descripcion = $"Pago del prestamo de numero XXXX{Prestamo.Codigo_Prestamo[^4..]}",
                    Monto = estadoPrestamo.Monto_pagado
                });

                Prestamo.Total_Pagado += estadoPrestamo.Monto_pagado;

                await _repositoryWrapper.PrestamoRepository.Update(Prestamo);

                estadoPrestamo.Monto_restante = Prestamo.Total_Pagado;

                await _repositoryWrapper.EstadoPrestamoRepository.Create(estadoPrestamo);

                await _repositoryWrapper.Save();

                await transaction.CommitAsync();



            }
            catch (UnnexpectedValueException ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
            catch (NoSufficientAmountException ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                throw new Exception("Hubo un problema al procesar la solicitud, si el problema perdura contacte a la administracion");
            }
        }

        public async Task Update(Prestamo prestamo)
        {
            var transaction = await _repositoryWrapper.BeginTransaction();

            await _repositoryWrapper.PrestamoRepository.Update(prestamo);
            await _repositoryWrapper.Save();

            await transaction.CommitAsync();
        }
    }
}
