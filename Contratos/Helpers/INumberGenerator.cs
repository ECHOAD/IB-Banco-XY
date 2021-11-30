using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratos.Helpers
{
    public interface INumberGenerator<T>
    {
        string Generate_a_Code();
    }
}
