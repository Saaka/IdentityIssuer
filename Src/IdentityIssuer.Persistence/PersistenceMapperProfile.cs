using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Persistence.Entities;

namespace IdentityIssuer.Persistence
{
    public class PersistenceMapperProfile : Profile
    {
        public PersistenceMapperProfile()
        {
            CreateMap<TenantEntity, Tenant>();
            CreateMap<TenantUserEntity, TenantUser>();
            CreateMap<CreateTenantDto, TenantEntity>();

            CreateMap<TenantAllowedOriginEntity, TenantAllowedOrigin>();
            
            CreateMap<CreateTenantSettingsDto, TenantSettingsEntity>();
            CreateMap<TenantSettingsEntity, TenantSettings>();
            
            CreateMap<TenantProviderSettingsEntity, TenantProviderSettings>();
        }
    }
}
