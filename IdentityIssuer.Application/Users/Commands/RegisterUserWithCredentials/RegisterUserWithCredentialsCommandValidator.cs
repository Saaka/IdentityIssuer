using FluentValidation;
using IdentityIssuer.Common.Constants;

namespace IdentityIssuer.Application.Users.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandValidator: AbstractValidator<RegisterUserWithCredentialsCommand>
    {
        public RegisterUserWithCredentialsCommandValidator()
        {
            RuleFor(x => x.DisplayName)
                .Length(UserConstants.MinDisplayNameLength, UserConstants.MaxDisplayNameLength)
                .NotEmpty();
            RuleFor(x => x.Password)
                .Length(UserConstants.MinPasswordLength, UserConstants.MaxPasswordLength)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(UserConstants.MinEmailLength, UserConstants.MaxPasswordLength);
        }
    }
}