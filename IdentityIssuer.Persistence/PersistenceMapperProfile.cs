using AutoMapper;
using IdentityIssuer.Application.Tenants;
using IdentityIssuer.Persistence.Entities;

namespace IdentityIssuer.Persistence
{
    public class PersistenceMapperProfile : Profile
    {
        public PersistenceMapperProfile()
        {
            CreateMap<TenantEntity, Tenant>();
            CreateMap<TenantConfigurationEntity, TenantConfiguration>();
            CreateMap<TenantProviderEntity, TenantProvider>();
        }
    }
}
