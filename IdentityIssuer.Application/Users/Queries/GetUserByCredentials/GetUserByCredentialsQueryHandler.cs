using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Users.Queries.GetUserByCredentials
{
    public class GetUserByCredentialsQueryHandler : IRequestHandler<GetUserByCredentialsQuery, AuthUserResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtTokenFactory jwtTokenFactory;
        private readonly ITenantsRepository tenantsRepository;
        private readonly IMapper mapper;

        public GetUserByCredentialsQueryHandler(
            IUserRepository userRepository,
            IJwtTokenFactory jwtTokenFactory,
            ITenantsRepository tenantsRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.jwtTokenFactory = jwtTokenFactory;
            this.tenantsRepository = tenantsRepository;
            this.mapper = mapper;
        }

        public async Task<AuthUserResult> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByCredentials(request.Email, request.Password,
                request.Tenant.TenantId);
            if (user == null)
                throw new UserNotFoundException(request.Email, request.Tenant.TenantCode);

            var settings = await tenantsRepository.GetTenantSettings(request.Tenant.TenantId);
            if (settings == null)
                throw new TenantSettingsNotFoundException(request.Tenant.TenantCode);

            var token = jwtTokenFactory.Create(user, settings, request.Tenant.TenantCode);
            return new AuthUserResult
            {
                Token = token,
                User = mapper.Map<UserDto>(user)
            };
        }
    }
}