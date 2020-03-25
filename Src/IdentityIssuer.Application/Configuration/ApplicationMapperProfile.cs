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
            CreateMap<TokenInfo, CreateUserDto>();
            CreateMap<Tenant, TenantDto>();

            CreateMap<CreateTenantProviderSettingsCommand, CreateTenantProviderSettingsDto>();
            CreateMap<TenantProviderSettings, TenantProviderSettingsDto>();
        }
    }
}