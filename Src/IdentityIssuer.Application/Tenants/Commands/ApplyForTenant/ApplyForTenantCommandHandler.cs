using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityIssuer.Application.Tenants.Models;
using IdentityIssuer.Application.Tenants.Repositories;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests;

namespace IdentityIssuer.Application.Tenants.Commands.ApplyForTenant
{
    public class ApplyForTenantCommandHandler : RequestHandler<ApplyForTenantCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly ITenantsRepository _tenantsRepository;
        private readonly ITenantApplicationsRepository _tenantApplicationsRepository;

        public ApplyForTenantCommandHandler(
            IMapper mapper,
            ITenantsRepository tenantsRepository,
            ITenantApplicationsRepository tenantApplicationsRepository)
        {
            _mapper = mapper;
            _tenantsRepository = tenantsRepository;
            _tenantApplicationsRepository = tenantApplicationsRepository;
        }
        
        public override async Task<RequestResult<Guid>> Handle(ApplyForTenantCommand request, CancellationToken cancellationToken)
        {
            if (await _tenantsRepository.TenantCodeExists(request.TenantCode))
                return RequestResult.Failure(ErrorCode.TenantAlreadyExistsForCode, 
                    new {TenantCode = request.TenantCode});

            var createApplicationData = _mapper.Map<SaveTenantApplicationData>(request);

            await _tenantApplicationsRepository.CreateTenantApplication(createApplicationData);
            
            return RequestResult.Success(request);
        }
    }
}