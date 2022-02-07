using System;
using System.Runtime.Serialization;

namespace Turnkey.exception
{
    [Serializable]
    public class TokenAcquirationException : Exception
    {
        public TokenAcquirationException()
        {
        }

        public TokenAcquirationException(string message) : base(message)
        {
        }

        public TokenAcquirationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TokenAcquirationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}