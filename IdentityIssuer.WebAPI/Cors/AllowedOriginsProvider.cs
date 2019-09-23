using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityIssuer.WebAPI.Cors
{
    public interface IAllowedOriginsProvider
    {
        Task<bool> IsOriginAvailable(string origin);
    }

    public class AllowedOriginsProvider : IAllowedOriginsProvider
    {
        private List<string> allowedOrigins = new List<string> { "http://localhost:8080" };
        
        public async Task<bool> IsOriginAvailable(string origin)
        {
            return allowedOrigins.Contains(origin);
        }
    }
}
