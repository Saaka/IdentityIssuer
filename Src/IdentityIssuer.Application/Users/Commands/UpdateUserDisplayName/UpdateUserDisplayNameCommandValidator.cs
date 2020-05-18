using FluentValidation;
using IdentityIssuer.Common.Constants;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Users.Commands.UpdateUserDisplayName
{
    public class UpdateUserDisplayNameCommandValidator : AbstractValidator<UpdateUserDisplayNameCommand>
    {
        public UpdateUserDisplayNameCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserNameRequired)
                .Length(UserConstants.MinDisplayNameLength, UserConstants.MaxDisplayNameLength)
                .WithMessageCode(ValidationErrorCode.UserNameInvalid);

            RuleFor(x => x.UserGuid)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserGuidRequired);

            RuleFor(x => x.RequestContext)
                .IsInUserContext();

            RuleFor(x => x.UserGuid)
                .Equal(x => x.RequestContext.User.UserGuid)
                .When(x => x.RequestContext.User != null && !x.RequestContext.User.IsAdmin)
                .WithMessageCode(ValidationErrorCode.UserActionNotAllowed);
        }
    }
}