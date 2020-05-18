using FluentValidation;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class RequestHasTenantContextValidator: AbstractValidator<RequestContextData>
    {
        public RequestHasTenantContextValidator()
        {
            RuleFor(x => x.Tenant)
                .SetValidator(new TenantContextValidator());
        }
    }
}