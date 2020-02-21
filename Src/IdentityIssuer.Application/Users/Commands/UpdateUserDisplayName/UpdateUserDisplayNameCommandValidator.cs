using FluentValidation;

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
        }
    }
}