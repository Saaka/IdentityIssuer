using System.Threading.Tasks;
using IdentityIssuer.Application.Tenants.Commands;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.WebAPI.Controllers.Tenants.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityIssuer.WebAPI.Controllers.Tenants
{
    [Route("api/[controller]")]
    public class TenantsController : BaseApiController
    {
        [HttpPost("create")]
        public async Task<ActionResult<TenantDto>> CreateTenant(CreateTenantModel model)
        {
            var adminContext = await GetAdminAsync();
            var result = await Mediator.Send(new CreateTenantCommand(
                model.Name, model.Code, model.AllowedOrigin, adminContext));

            return GetResponse(result);
        }
    }
}