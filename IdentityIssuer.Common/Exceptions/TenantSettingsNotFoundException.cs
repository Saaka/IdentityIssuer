using System;

namespace IdentityIssuer.Common.Exceptions
{
    public class TenantSettingsNotFoundException: ArgumentException
    {
        public TenantSettingsNotFoundException(int tenantId)
            : base($"Tenant settings {tenantId} not found")
        {
        }
    }
}