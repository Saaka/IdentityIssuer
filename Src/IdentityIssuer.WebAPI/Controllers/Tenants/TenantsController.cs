using System;
using System.Threading.Tasks;
using IdentityIssuer.Application.Tenants.Commands;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.WebAPI.Controllers.Tenants.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityIssuer.WebAPI.Controllers.Tenants
{
    [Authorize]
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

        [HttpPost("settings/update")]
        public async Task<ActionResult<TenantSettingsDto>> UpdateTenantSettings(UpdateTenantSettingsModel model)
        {
            var adminContext = await GetAdminAsync();
            var command = Mapper.Map<UpdateTenantSettingsCommand>(model)
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

        [HttpPost("providerSettings/update")]
        public async Task<ActionResult<TenantProviderSettingsDto>> UpdateProviderSettings(UpdateProviderSettingsModel model)
        {
            var adminContext = await GetAdminAsync();
            var command = Mapper.Map<UpdateTenantProviderSettingsCommand>(model)
                .WithAdminContextData(adminContext);
            
            var result = await Mediator.Send(command);

            return GetResponse(result);
        }

        [HttpDelete("providerSettings/delete")]
        public async Task<ActionResult<Guid>> DeleteProviderSettings(DeleteProviderSettingsModel model)
        {
            var adminContext = await GetAdminAsync();
            var command = Mapper.Map<DeleteTenantProviderSettingsCommand>(model)
                .WithAdminContextData(adminContext);
            
            var result = await Mediator.Send(command);

            return GetResponse(result);
        }

        [HttpPost("application")]
        public async Task<ActionResult<TenantApplicationDto>> ApplyForTenant(TenantApplicationModel model)
        {
            var adminTenantContext = await GetAdminTenantAsync();
            var command = Mapper.Map<ApplyForTenantCommand>(model)
                .WithAdminTenantContextData(adminTenantContext);

            var result = await Mediator.Send(command);

            return GetResponse(result);
        }
    }
}