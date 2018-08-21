using LCU.NET.API_Models;
using System;
using System.Runtime.Serialization;

namespace LCU.NET
{
    public class APIErrorException : Exception
    {
        public ErrorData Error { get; }

        public APIErrorException() : base()
        {
        }
        public APIErrorException(string message) : base(message)
        {
        }
        public APIErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected APIErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public APIErrorException(ErrorData error) : base(error.Message)
        {
            this.Error = error;
        }
    }
}
