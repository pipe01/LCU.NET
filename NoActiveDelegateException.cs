using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET
{
    public class NoActiveDelegateException : APIErrorException
    {
        public NoActiveDelegateException() : base()
        {
        }

        public NoActiveDelegateException(string message) : base(message)
        {
        }

        public NoActiveDelegateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoActiveDelegateException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public NoActiveDelegateException(API_Models.ErrorData error) : base(error)
        {
        }
    }
}
