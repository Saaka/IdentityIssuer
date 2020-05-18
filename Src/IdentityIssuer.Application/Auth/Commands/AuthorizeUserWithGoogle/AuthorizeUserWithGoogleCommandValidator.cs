using FluentValidation;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Auth.Commands.AuthorizeUserWithGoogle
{
    public class AuthorizeUserWithGoogleCommandValidator : AbstractValidator<AuthorizeUserWithGoogleCommand>
    {
        public AuthorizeUserWithGoogleCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.ProviderTokenRequired);

            RuleFor(x => x.RequestContext)
                .IsInTenantContext();
        }   
    }
}