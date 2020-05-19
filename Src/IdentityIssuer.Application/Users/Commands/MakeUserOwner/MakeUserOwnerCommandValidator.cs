using FluentValidation;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Users.Commands.MakeUserOwner
{
    public class MakeUserOwnerCommandValidator : AbstractValidator<MakeUserOwnerCommand>
    {
        public MakeUserOwnerCommandValidator()
        {
            RuleFor(x => x.UserGuid)
                .NotEmpty()
                .WithMessageCode(ValidationErrorCode.UserGuidRequired);
            RuleFor(x => x.RequestContext)
                .IsInAdminContext();
        }
    }
}