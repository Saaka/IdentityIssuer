using FluentValidation;
using IdentityIssuer.Application.Validators.FluentValidation;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContexts;

namespace IdentityIssuer.Application
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, RequestContext> IsInUserContext<T>(
            this IRuleBuilder<T, RequestContext> ruleBuilder)
            => ruleBuilder.SetValidator(new IsInUserContextValidator());

        public static IRuleBuilderOptions<T, RequestContext> IsInTenantContext<T>(
            this IRuleBuilder<T, RequestContext> ruleBuilder)
            => ruleBuilder.SetValidator(new IsInTenantContextValidator());

        public static IRuleBuilderOptions<T, RequestContext> IsInAdminTenantContext<T>(
            this IRuleBuilder<T, RequestContext> ruleBuilder)
            => ruleBuilder.SetValidator(new IsInAdminTenantContextValidator());

        public static IRuleBuilderOptions<T, RequestContext> IsInAdminContext<T>(
            this IRuleBuilder<T, RequestContext> ruleBuilder)
            => ruleBuilder.SetValidator(new IsInAdminContextValidator());

        public static IRuleBuilderOptions<T, TProperty> WithMessageCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ValidationErrorCode code)
            => rule.WithMessage(code.ToString());
    }
}