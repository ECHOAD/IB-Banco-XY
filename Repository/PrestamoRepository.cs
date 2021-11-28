using Capo_Datos;
using Contratos.Repository_Contracts;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PrestamoRepository : RepositoryBase<Prestamo, int>, IPrestamoRepository
    {
        public PrestamoRepository(InternetBanking internetBankingContext) : base(internetBankingContext)
        {
        }
    }
}
