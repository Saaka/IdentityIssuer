using FluentValidation;

namespace IdentityIssuer.Application.Users.Commands.AuthorizeUserWithGoogle
{
    public class AuthorizeUserWithGoogleCommandValidator : AbstractValidator<AuthorizeUserWithGoogleCommand>
    {
        public AuthorizeUserWithGoogleCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty();
            RuleFor(x => x.Tenant)
                .IsValid();
        }   
    }
}