using FluentValidation;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContexts;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class TenantContextValidator : AbstractValidator<TenantContext>
    {
        public TenantContextValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessageCode(ValidationErrorCode.TenantContextRequired);
            RuleFor(x => x.TenantCode)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantContextRequired);
            RuleFor(x=> x.TenantId)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.TenantContextRequired);
        }
    }
}