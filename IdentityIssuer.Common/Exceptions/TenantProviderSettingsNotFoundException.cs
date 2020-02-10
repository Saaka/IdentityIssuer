using System;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Common.Exceptions
{
    public class TenantProviderSettingsNotFoundException : ArgumentException
    {
        public TenantProviderSettingsNotFoundException(string tenantCode, AuthProviderType providerType)
            : base($"Tenant \"{tenantCode}\" settings for \"{providerType.ToString()}\" provider not found ")
        {
        }
    }
}