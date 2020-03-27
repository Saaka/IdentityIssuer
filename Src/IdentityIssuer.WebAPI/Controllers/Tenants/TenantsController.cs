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
            var command = Mapper.Map<CreateTenantCommand>(model)
                .WithAdminContextData(adminContext);
            
            var result = await Mediator.Send(command);

            return GetResponse(result);
        }

        [HttpPost("providerSettings/create")]
        public async Task<ActionResult<TenantProviderSettingsDto>> CreateProviderSettings(CreateProviderSettingsModel model)
        {
            var adminContext = await GetAdminAsync();
            var command = Mapper.Map<CreateTenantProviderSettingsCommand>(model)
                .WithAdminContextData(adminContext);
            
            var result = await Mediator.Send(command);

            return GetResponse(result);
        }

        [HttpPost("settings/update")]
        public async Task<ActionResult<TenantSettingsDto>> UpdateTenantSettings(UpdateTenantSettingsModel model)
        {
            var adminContext = await GetAdminAsync();
            var command = Mapper.Map<UpdateTenantSettingsCommand>(model)
                .WithAdminContextData(adminContext);

            var result = await Mediator.Send(command);

            return GetResponse(result);
        }
    }
}