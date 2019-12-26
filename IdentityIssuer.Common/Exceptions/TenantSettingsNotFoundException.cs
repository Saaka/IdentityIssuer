using System;

namespace IdentityIssuer.Common.Exceptions
{
    public class TenantSettingsNotFoundException: ArgumentException
    {
        public TenantSettingsNotFoundException(string tenantCode)
            : base($"Tenant settings {tenantCode} not found")
        {
        }
    }
}