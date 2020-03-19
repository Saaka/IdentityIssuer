using System;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Common.Exceptions
{
    public class DomainException : ArgumentException
    {
        public ErrorCode ErrorCode { get; }
        public object ErrorDetails { get; }

        public DomainException(ErrorCode code, object details = null)
            : base(code.ToString())
        {
            ErrorCode = code;
            ErrorDetails = details;
        }
    }
}