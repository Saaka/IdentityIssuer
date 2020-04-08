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

            CreateMap<CreateTenantData, TenantEntity>()
                .ForMember(t => t.Code,
                    cfg => cfg.MapFrom(d => d.TenantCode));

            CreateMap<TenantAllowedOriginEntity, TenantAllowedOrigin>();

            CreateMap<SaveTenantSettingsData, TenantSettingsEntity>();
            CreateMap<TenantSettingsEntity, TenantSettings>();

            CreateMap<TenantProviderSettingsEntity, TenantProviderSettings>();
            CreateMap<CreateTenantProviderSettingsData, TenantProviderSettingsEntity>();
        }
    }
}