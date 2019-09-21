using System.Threading.Tasks;

namespace IdentityIssuer.WebAPI.Cors
{
    public interface ITenantOriginProvider
    {
        Task<string> GetAllowedOrigin(string tenantCode);
    }

    public class TenantOriginProvider : ITenantOriginProvider
    {
        public async Task<string> GetAllowedOrigin(string tenantCode)
        {
            return "http://localhost:8080";
        }
    }
}
