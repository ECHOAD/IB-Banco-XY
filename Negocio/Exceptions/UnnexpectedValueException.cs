using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Exceptions
{
    public class UnnexpectedValueException : Exception
    {
        public UnnexpectedValueException()
        {
        }

        public UnnexpectedValueException(string message) : base(message)
        {
        }

        public UnnexpectedValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnnexpectedValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
