using FluentValidation;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Auth.Commands.AuthorizeUserWithFacebook
{
    public class AuthorizeUserWithFacebookCommandValidator : AbstractValidator<AuthorizeUserWithFacebookCommand>
    {
        public AuthorizeUserWithFacebookCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.ProviderTokenRequired);

            RuleFor(x => x.RequestContext)
                .HasTenantContext();
        }   
    }
}