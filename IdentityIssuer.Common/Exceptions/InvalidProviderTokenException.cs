using System;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Common.Exceptions
{
    public class InvalidProviderTokenException : Exception
    {
        public InvalidProviderTokenException(AuthProviderType providerType)
            : base($"Invalid token for {providerType} provider")
        {
        }
    }
}