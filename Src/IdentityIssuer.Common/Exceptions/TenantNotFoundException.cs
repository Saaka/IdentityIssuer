using System;

namespace IdentityIssuer.Common.Exceptions
{
    public class TenantNotFoundException : ArgumentException
    {
        public TenantNotFoundException(string tenantCode)
            : base($"Tenant {tenantCode} not found")
        {
        }
    }
}
