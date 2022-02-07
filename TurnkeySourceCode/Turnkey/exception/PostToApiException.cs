using System;
using System.Runtime.Serialization;

namespace Turnkey.exception
{
    [Serializable]
    public class PostToApiException : Exception
    {
        public PostToApiException()
        {
        }

        public PostToApiException(string message) : base(message)
        {
        }

        public PostToApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PostToApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}