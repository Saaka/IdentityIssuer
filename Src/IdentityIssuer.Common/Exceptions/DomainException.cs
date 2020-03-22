using System;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Common.Exceptions
{
    public class DomainException : ArgumentException
    {
        public ErrorCode ErrorCode { get; }
        public object ErrorDetails { get; }

        public DomainException(ErrorCode code, object details = null)
            : base(code.ToString())
            => (ErrorCode, ErrorDetails) = (code, details);

        public DomainException(RequestResult requestResult)
            : this(requestResult.Error, requestResult.ErrorDetails)
        {
        }
    }
}