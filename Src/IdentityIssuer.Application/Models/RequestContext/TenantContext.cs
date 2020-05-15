namespace IdentityIssuer.Application.Models.RequestContext
{
    public class TenantContext
    {
        public int TenantId { get; }
        public string TenantCode { get; }
        public bool IsAdminTenant { get; }
    }
}