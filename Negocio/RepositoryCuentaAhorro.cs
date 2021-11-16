using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capo_Datos;
using Contratos;
using Entidades;

namespace Negocio
{
    public class RepositoryCuentaAhorro : IRepository<CuentasAhorro, int>
    {

        public RepositoryCuentaAhorro( )
        {

        }
        public Task delete(CuentasAhorro entity)
        {
            throw new NotImplementedException();
        }

        public Task<CuentasAhorro> findEntity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<int>> getEntity()
        {
            throw new NotImplementedException();
        }

        public Task saveEntity(CuentasAhorro entity)
        {
            throw new NotImplementedException();
        }

        public Task updateEntity(CuentasAhorro entity)
        {
            throw new NotImplementedException();
        }
    }
}
