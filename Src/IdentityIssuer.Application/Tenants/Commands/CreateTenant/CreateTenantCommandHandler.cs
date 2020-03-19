using System.Threading;
using System.Threading.Tasks;
using IdentityIssuer.Application.Auth.Commands;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using MediatR;

namespace IdentityIssuer.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, CreateTenantResult>
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly IGuid _guid;


        public CreateTenantCommandHandler(
            ITenantsRepository tenantsRepository,
            IUserRepository userRepository,
            IMediator mediator,
            IGuid guid)
        {
            _tenantsRepository = tenantsRepository;
            _userRepository = userRepository;
            _mediator = mediator;
            _guid = guid;
        }

        public async Task<CreateTenantResult> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            if (await _tenantsRepository.TenantCodeExists(request.Code))
                throw new DomainException(ErrorCode.TenantAlreadyExistsForCode, new {Code = request.Code});

            var tenant = await _tenantsRepository
                .CreateTenant(new CreateTenantDto(request.Name, request.Code, request.AllowedOrigin));
            
            return new CreateTenantResult(true, tenant);
        }
    }
}