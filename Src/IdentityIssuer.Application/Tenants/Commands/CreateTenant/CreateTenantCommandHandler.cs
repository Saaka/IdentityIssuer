using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Application.Users.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : RequestHandler<CreateTenantCommand, TenantDto>
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public CreateTenantCommandHandler(
            ITenantsRepository tenantsRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _tenantsRepository = tenantsRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public override async Task<RequestResult<TenantDto>> Handle(CreateTenantCommand request,
            CancellationToken cancellationToken)
        {
            if (await _tenantsRepository.TenantCodeExists(request.Code))
                throw new DomainException(ErrorCode.TenantAlreadyExistsForCode, new {Code = request.Code});

            var tenant = await _tenantsRepository
                .CreateTenant(new CreateTenantDto(request.Name, request.Code, request.AllowedOrigin));

            return RequestResult<TenantDto>
                .Success(_mapper.Map<TenantDto>(tenant));
        }
    }
}