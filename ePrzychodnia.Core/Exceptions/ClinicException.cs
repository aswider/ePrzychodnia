using System;

namespace ePrzychodnia.Core.Exceptions
{
    public class ClinicException : Exception
    {
        public ClinicException(ErrorCodes errorCode, Exception sourceException = null)
        {
            SourceException = sourceException;
            ErrorCode = errorCode;
        }

        public Exception SourceException { get; set; }

        public ErrorCodes ErrorCode { get; set; }
    }
}