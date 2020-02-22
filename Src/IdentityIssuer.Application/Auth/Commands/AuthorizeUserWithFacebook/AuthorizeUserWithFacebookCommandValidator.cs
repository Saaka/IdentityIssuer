using FluentValidation;

namespace IdentityIssuer.Application.Auth.Commands.AuthorizeUserWithFacebook
{
    public class AuthorizeUserWithFacebookCommandValidator : AbstractValidator<AuthorizeUserWithFacebookCommand>
    {
        public AuthorizeUserWithFacebookCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty();
            RuleFor(x => x.Tenant)
                .IsValid();
        }   
    }
}