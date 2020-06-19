using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet.Exceptions
{
    public class CapMonsterException : Exception
    {
        public int ErrorId { get; set; }
        
        public ErrorType ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        public CapMonsterException() : base()
        {
        }

        public CapMonsterException(int errorId, ErrorType errorCode, string errorDescription) : base()
        {
            ErrorId = errorId;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
