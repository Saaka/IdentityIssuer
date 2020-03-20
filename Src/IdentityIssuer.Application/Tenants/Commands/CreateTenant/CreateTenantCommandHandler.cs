using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Requests;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;

namespace IdentityIssuer.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : RequestHandler<CreateTenantCommand, Tenant>
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGuid _guid;


        public CreateTenantCommandHandler(
            ITenantsRepository tenantsRepository,
            IUserRepository userRepository,
            IGuid guid)
        {
            _tenantsRepository = tenantsRepository;
            _userRepository = userRepository;
            _guid = guid;
        }

        public override async Task<RequestResult<Tenant>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            if (await _tenantsRepository.TenantCodeExists(request.Code))
                throw new DomainException(ErrorCode.TenantAlreadyExistsForCode, new {Code = request.Code});

            var tenant = await _tenantsRepository
                .CreateTenant(new CreateTenantDto(request.Name, request.Code, request.AllowedOrigin));

            return RequestResult<Tenant>
                .Success(tenant);
        }
    }
}