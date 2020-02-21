using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Infrastructure.Security.Google;

namespace IdentityIssuer.Infrastructure
{
    public class InfrastructureAutoMapperProfile : Profile
    {
        public InfrastructureAutoMapperProfile()
        {
            CreateMap<GoogleTokenInfo, TokenInfo>()
                .ForMember(x => x.ExternalUserId, c => c.MapFrom(t => t.GoogleUserId));
        }
    }
}