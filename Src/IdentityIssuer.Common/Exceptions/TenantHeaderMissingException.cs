using System;

namespace IdentityIssuer.Common.Exceptions
{
    public class TenantHeaderMissingException : ArgumentException
    {
        public TenantHeaderMissingException()
            : base("Tenant header was not found")
        {
        }
    }
}