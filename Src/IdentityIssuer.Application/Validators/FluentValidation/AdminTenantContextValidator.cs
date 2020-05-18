using FluentValidation;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class AdminTenantContextValidator :  AbstractValidator<TenantContext>
    {
        public AdminTenantContextValidator()
        {
            RuleFor(x=> x.IsAdminTenant)
                .Equal(true)
                .WithMessageCode(ValidationErrorCode.AdminTenantContextRequired);
        }
    }
}