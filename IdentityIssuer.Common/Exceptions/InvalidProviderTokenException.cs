using System;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Common.Exceptions
{
    public class InvalidProviderTokenException : InvalidOperationException
    {
        public InvalidProviderTokenException(AuthProviderType providerType, string tenantCode)
            : base($"Invalid {providerType} token for tenant \"{tenantCode}\"")
        {
        }
    }
}