using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Services;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.ApplyForTenant
{
    public class ApplyForTenantCommandHandler : RequestHandler<ApplyForTenantCommand, TenantApplicationDto>
    {
        private readonly IMapper _mapper;
        private readonly IGuid _guid;
        private readonly ITenantsRepository _tenantsRepository;
        private readonly ITenantApplicationsRepository _tenantApplicationsRepository;

        public ApplyForTenantCommandHandler(
            IMapper mapper,
            IGuid guid,
            ITenantsRepository tenantsRepository,
            ITenantApplicationsRepository tenantApplicationsRepository)
        {
            _mapper = mapper;
            _guid = guid;
            _tenantsRepository = tenantsRepository;
            _tenantApplicationsRepository = tenantApplicationsRepository;
        }

        public override async Task<RequestResult<TenantApplicationDto>> Handle(ApplyForTenantCommand request,
            CancellationToken cancellationToken)
        {
            if (await _tenantsRepository.TenantCodeExists(request.TenantCode))
                return RequestResult<TenantApplicationDto>.Failure(ErrorCode.TenantAlreadyExistsForCode,
                    new {TenantCode = request.TenantCode});

            var createApplicationData = _mapper.Map<SaveTenantApplicationData>(request);
            createApplicationData.TenantApplicationGuid = _guid.GetGuid();

            var tenantApplication = await _tenantApplicationsRepository
                .CreateTenantApplication(createApplicationData);

            return RequestResult<TenantApplicationDto>
                .Success(_mapper.Map<TenantApplicationDto>(tenantApplication));
        }
    }
}