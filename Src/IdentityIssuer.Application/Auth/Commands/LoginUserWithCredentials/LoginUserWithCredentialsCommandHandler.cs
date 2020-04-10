using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Auth.Models;
using IdentityIssuer.Application.Auth.Repositories;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Models;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Auth.Commands.LoginUserWithCredentials
{
    public class LoginUserWithCredentialsCommandHandler: RequestHandler<LoginUserWithCredentialsCommand, AuthorizationData>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenFactory _jwtTokenFactory;
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IMapper _mapper;

        public LoginUserWithCredentialsCommandHandler(
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

        public override async Task<RequestResult<AuthorizationData>> Handle(LoginUserWithCredentialsCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _authRepository.GetUserByCredentials(request.Email, request.Password,
                request.Tenant.TenantId);
            if (user == null)
                return RequestResult<AuthorizationData>.Failure(ErrorCode.UserNotFound,
                    new {email = request.Email, tenantCode = request.Tenant.TenantCode});

            var settings = await _tenantsRepository.GetTenantSettings(request.Tenant.TenantId);
            if (settings == null)
                return RequestResult<AuthorizationData>.Failure(ErrorCode.TenantSettingsNotFound,
                    new {tenantCode = request.Tenant.TenantCode});

            var token = _jwtTokenFactory.Create(user, settings, request.Tenant.TenantCode);
            
            return RequestResult<AuthorizationData>
                .Success(new AuthorizationData
                {
                    Token = token,
                    User = _mapper.Map<UserDto>(user),
                    TenantCode = request.Tenant.TenantCode
                });
        }
    }
}