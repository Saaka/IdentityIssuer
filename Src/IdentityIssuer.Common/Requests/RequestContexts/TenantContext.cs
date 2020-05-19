namespace IdentityIssuer.Common.Requests.RequestContexts
{
    public class TenantContext
    {
        public TenantContext(int tenantId, string tenantCode, bool isAdminTenant)
        {
            TenantId = tenantId;
            TenantCode = tenantCode;
            IsAdminTenant = isAdminTenant;
        }
        
        public int TenantId { get; }
        public string TenantCode { get; }
        public bool IsAdminTenant { get; }
    }
}