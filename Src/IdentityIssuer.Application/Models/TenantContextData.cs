namespace IdentityIssuer.Application.Models
{
    public class TenantContextData
    {
        public TenantContextData(int tenantId, string tenantCode)
        {
            TenantId = tenantId;
            TenantCode = tenantCode;
        }
        
        public int TenantId { get; }
        public string TenantCode { get; }
    }
}