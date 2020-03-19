using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Requests;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Auth.Queries.GetUserByCredentials
{
    public class GetUserByCredentialsQueryHandler : RequestHandler<GetUserByCredentialsQuery, AuthUserResult>
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

        public override async Task<RequestResult<AuthUserResult>> Handle(GetUserByCredentialsQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _authRepository.GetUserByCredentials(request.Email, request.Password,
                request.Tenant.TenantId);
            if (user == null)
                return RequestResult<AuthUserResult>.Failure(ErrorCode.UserNotFound,
                    new {email = request.Email, tenantCode = request.Tenant.TenantCode});

            var settings = await _tenantsRepository.GetTenantSettings(request.Tenant.TenantId);
            if (settings == null)
                return RequestResult<AuthUserResult>.Failure(ErrorCode.TenantSettingsNotFound,
                    new {tenantCode = request.Tenant.TenantCode});

            var token = _jwtTokenFactory.Create(user, settings, request.Tenant.TenantCode);
            
            return RequestResult<AuthUserResult>
                .Success(new AuthUserResult
                {
                    Token = token,
                    User = _mapper.Map<UserDto>(user)
                });
        }
    }
}