using FluentValidation;

namespace IdentityIssuer.Application.Users.Commands.RegisterUserWithCredentials
{
    public class RegisterUserWithCredentialsCommandValidator: AbstractValidator<RegisterUserWithCredentialsCommand>
    {
        public RegisterUserWithCredentialsCommandValidator()
        {
            RuleFor(x => x.DisplayName)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}