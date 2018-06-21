using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET.API_Models
{
    public class ErrorData
    {
        public string ErrorCode { get; }
        public int HttpStatus { get; }
        public string Message { get; }

        [JsonConstructor]
        internal ErrorData(string errorCode, int httpStatus, string message)
        {
            this.ErrorCode = errorCode;
            this.HttpStatus = httpStatus;
            this.Message = message;
        }
    }
}
