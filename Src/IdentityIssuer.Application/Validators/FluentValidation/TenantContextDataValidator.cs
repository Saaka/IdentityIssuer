using FluentValidation;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class TenantContextDataValidator : AbstractValidator<TenantContextData>
    {
        public TenantContextDataValidator()
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