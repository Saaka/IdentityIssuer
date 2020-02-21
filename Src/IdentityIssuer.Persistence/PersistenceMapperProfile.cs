using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Persistence.Entities;

namespace IdentityIssuer.Persistence
{
    public class PersistenceMapperProfile : Profile
    {
        public PersistenceMapperProfile()
        {
            CreateMap<TenantEntity, Tenant>();
            CreateMap<TenantSettingsEntity, TenantSettings>();
            CreateMap<TenantProviderSettingsEntity, TenantProviderSettings>();
            CreateMap<TenantUserEntity, TenantUser>();
        }
    }
}
