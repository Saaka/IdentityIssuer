namespace IdentityIssuer.Application.Models
{
    public class AdminTenantContextData
    {
        public AdminTenantContextData(int tenantId, string tenantCode, bool isAdminTenant)
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