using FluentValidation;
using IdentityIssuer.Common.Requests.RequestContexts;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class IsInTenantContextValidator : AbstractValidator<RequestContext>
    {
        public IsInTenantContextValidator()
        {
            RuleFor(x => x.Tenant)
                .SetValidator(new TenantContextValidator());
        }
    }
}