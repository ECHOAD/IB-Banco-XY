using System;
using System.Runtime.Serialization;

namespace Negocio.Exceptions
{
    public class NoSufficientAmountException : Exception
    {
        public NoSufficientAmountException()
        {
        }

        public NoSufficientAmountException(string message) : base(message)
        {
        }

        public NoSufficientAmountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSufficientAmountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
