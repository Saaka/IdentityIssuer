using FluentValidation;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application.Users.Commands.UpdateUserDisplayName
{
    public class UpdateUserDisplayNameCommandValidator : AbstractValidator<UpdateUserDisplayNameCommand>
    {
        public UpdateUserDisplayNameCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.UserGuid)
                .NotEmpty();
            RuleFor(x => x.User)
                .IsValid();

            RuleFor(x => x.UserGuid)
                .Equal(x => x.User.UserGuid)
                .WithErrorCode(ErrorCode.UserClaimMissing.ToString())
                .When(x=> !x.User.IsAdmin);
        }
    }
}