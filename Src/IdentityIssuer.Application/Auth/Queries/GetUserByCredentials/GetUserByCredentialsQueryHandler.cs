using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Auth.Queries.GetUserByCredentials
{
    public class GetUserByCredentialsQueryHandler : IRequestHandler<GetUserByCredentialsQuery, AuthUserResult>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenFactory _jwtTokenFactory;
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IMapper _mapper;

        public GetUserByCredentialsQueryHandler(
            IAuthRepository authRepository,
            IJwtTokenFactory jwtTokenFactory,
            ITenantsRepository tenantsRepository,
            IMapper mapper)
        {
            _authRepository = authRepository;
            _jwtTokenFactory = jwtTokenFactory;
            _tenantsRepository = tenantsRepository;
            _mapper = mapper;
        }

        public async Task<AuthUserResult> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken)
        {
            var user = await _authRepository.GetUserByCredentials(request.Email, request.Password,
                request.Tenant.TenantId);
            if (user == null)
                throw new DomainException(ErrorCode.UserNotFound,
                    new { email = request.Email, tenantCode = request.Tenant.TenantCode });

            var settings = await _tenantsRepository.GetTenantSettings(request.Tenant.TenantId);
            if (settings == null)
                throw new DomainException(ErrorCode.TenantSettingsNotFound, 
                    new { tenantCode = request.Tenant.TenantCode });

            var token = _jwtTokenFactory.Create(user, settings, request.Tenant.TenantCode);
            return new AuthUserResult
            {
                Token = token,
                User = _mapper.Map<UserDto>(user)
            };
        }
    }
}