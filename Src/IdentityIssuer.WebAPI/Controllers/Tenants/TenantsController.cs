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
            var command = Mapper.Map<CreateTenantCommand>(model)
                .WithAdminContextData(await GetAdminAsync());
            
            var result = await Mediator.Send(command);

            return GetResponse(result);
        }

        [HttpPost("providersettings/create")]
        public async Task<ActionResult<TenantProviderSettingsDto>> CreateProviderSettings(CreateProviderSettingsModel model)
        {
            var command = Mapper.Map<CreateTenantProviderSettingsCommand>(model)
                .WithAdminContextData(await GetAdminAsync());
            
            var result = await Mediator.Send(command);

            return GetResponse(result);
        }
    }
}