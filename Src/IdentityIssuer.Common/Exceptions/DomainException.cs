using System;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Common.Exceptions
{
    public class DomainException : ArgumentException
    {
        public ExceptionCode ExceptionCode { get; }
        public object ExceptionDetails { get; }

        public DomainException(ExceptionCode code, object details = null)
            : base(code.ToString())
        {
            ExceptionCode = code;
            ExceptionDetails = details;
        }
    }
}