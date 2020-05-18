using FluentValidation;

namespace IdentityIssuer.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.RequestContext)
                .HasUserContext();
        }
    }
}