using FluentValidation;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Auth.Commands.LoginUserWithCredentials
{
    public class LoginUserWithCredentialsCommandValidator: AbstractValidator<LoginUserWithCredentialsCommand>
    {
        public LoginUserWithCredentialsCommandValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserPasswordRequired);
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserEmailRequired)
                .EmailAddress()
                .WithMessageCode(ValidationErrorCode.UserEmailInvalid);
            RuleFor(x => x.RequestContext)
                .HasTenantContext();
        }
    }
}