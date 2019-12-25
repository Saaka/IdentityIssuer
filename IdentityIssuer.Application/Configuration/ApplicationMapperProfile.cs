using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Users.Models;

namespace IdentityIssuer.Application.Configuration
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            CreateMap<TenantUser, UserDto>();
        }
    }
}