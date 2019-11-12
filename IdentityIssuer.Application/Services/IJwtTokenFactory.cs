using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.Services
{
    public interface IJwtTokenFactory
    {
        string Create(TenantUser user, TenantSettings settings);
    }
}