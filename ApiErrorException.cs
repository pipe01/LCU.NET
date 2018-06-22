using LCU.NET.API_Models;
using System;
using System.Runtime.Serialization;

namespace LCU.NET
{
    public class ApiErrorException : Exception
    {
        public ErrorData Error { get; }

        public ApiErrorException() : base()
        {
        }
        public ApiErrorException(string message) : base(message)
        {
        }
        public ApiErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected ApiErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ApiErrorException(ErrorData error) : base(error.Message)
        {
            this.Error = error;
        }
    }
}
