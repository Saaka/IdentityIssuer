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
                .WithMessageCode(ErrorCode.UserNameRequired)
                .MinimumLength(4)
                .WithMessage("MinNameLen");

            RuleFor(x => x.UserGuid)
                .NotEmpty()
                .WithMessageCode(ErrorCode.UserGuidMissing);

            RuleFor(x => x.UserGuid)
                .Equal(x => x.User.UserGuid)
                .WithMessageCode(ErrorCode.ActionNotAllowedByUser)
                .When(x => !x.User.IsAdmin);

            RuleFor(x => x.User)
                .IsValid();
        }
    }
}