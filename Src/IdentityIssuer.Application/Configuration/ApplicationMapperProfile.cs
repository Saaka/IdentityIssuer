using AutoMapper;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Commands;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Configuration
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            CreateMap<TenantUser, UserDto>();
            CreateMap<TokenInfo, CreateUserData>();
            CreateMap<Tenant, TenantDto>();
            CreateMap<TenantSettings, TenantSettingsDto>();

            CreateMap<CreateTenantProviderSettingsCommand, CreateTenantProviderSettingsData>();
            CreateMap<TenantProviderSettings, TenantProviderSettingsDto>();
            CreateMap<UpdateTenantProviderSettingsCommand, UpdateTenantProviderSettingsData>();

            CreateMap<CreateTenantCommand, SaveTenantSettingsData>();
            CreateMap<CreateTenantCommand, CreateTenantData>();

            CreateMap<ApplyForTenantCommand, SaveTenantApplicationData>();
            CreateMap<TenantApplication, TenantApplicationDto>();
            
            CreateMap<UpdateTenantSettingsCommand, SaveTenantSettingsData>();
        }
    }
}