using System;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Common.Exceptions
{
    public class DomainException : ArgumentException
    {
        public ExceptionCode ExceptionCode { get; private set; }

        public DomainException(ExceptionCode code, string details = null)
            : base(details ?? code.ToString())
        {
            ExceptionCode = code;
        }
    }
}