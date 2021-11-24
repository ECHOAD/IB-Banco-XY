using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratos
{
    public interface IRepositoryWrapper
    {

        public ITarjetaCreditoRepository TarjetaCreditoRepository { get; }

        public IEstadoPrestamoRepository EstadoCreditoRepository { get; }

        public IEstadoCuentaRepository EstadoCuentaRepository { get; }

        public ICuentaAhorroRepository CuentaAhorroRepository { get; }

        public IPrestamoRepository PrestamoRepository { get; }


        Task Save();




    }
}
