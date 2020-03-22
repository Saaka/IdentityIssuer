using FluentValidation;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Validators.FluentValidation
{
    public class AdminContextDataValidator : AbstractValidator<AdminContextData>
    {
        public AdminContextDataValidator()
        {
            RuleFor(x => x.ContextType)
                .IsInEnum()
                .NotEqual(AdminContextType.None)
                .WithMessageCode(ValidationErrorCode.AdminContextRequired);

            RuleFor(x => x.UserId)
                .NotNull()
                .GreaterThan(0)
                .When(x => x.ContextType == AdminContextType.User)
                .WithMessageCode(ValidationErrorCode.UserActionNotAllowed);
        }
    }
}