using FluentValidation;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class IsInTenantContextValidator : AbstractValidator<RequestContextData>
    {
        public IsInTenantContextValidator()
        {
            RuleFor(x => x.Tenant)
                .SetValidator(new TenantContextValidator());
        }
    }
}