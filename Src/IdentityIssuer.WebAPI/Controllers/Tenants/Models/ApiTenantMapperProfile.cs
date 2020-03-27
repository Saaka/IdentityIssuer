using AutoMapper;
using IdentityIssuer.Application.Tenants.Commands;

namespace IdentityIssuer.WebAPI.Controllers.Tenants.Models
{
    public class ApiTenantMapperProfile : Profile
    {
        public ApiTenantMapperProfile()
        {
            CreateMap<CreateTenantModel, CreateTenantCommand>();
            CreateMap<CreateProviderSettingsModel, CreateTenantProviderSettingsCommand>();
            CreateMap<UpdateTenantSettingsModel, UpdateTenantSettingsCommand>();
        }
    }
}