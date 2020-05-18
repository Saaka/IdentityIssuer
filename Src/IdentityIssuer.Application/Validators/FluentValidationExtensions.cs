using FluentValidation;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Validators.FluentValidation;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, RequestContextData> IsInUserContext<T>(
            this IRuleBuilder<T, RequestContextData> ruleBuilder)
            => ruleBuilder.SetValidator(new IsInUserContextValidator());

        public static IRuleBuilderOptions<T, RequestContextData> IsInTenantContext<T>(
            this IRuleBuilder<T, RequestContextData> ruleBuilder)
            => ruleBuilder.SetValidator(new IsInTenantContextValidator());

        public static IRuleBuilderOptions<T, RequestContextData> IsInAdminTenantContext<T>(
            this IRuleBuilder<T, RequestContextData> ruleBuilder)
            => ruleBuilder.SetValidator(new IsInAdminTenantContextValidator());

        public static IRuleBuilderOptions<T, RequestContextData> IsInAdminContext<T>(
            this IRuleBuilder<T, RequestContextData> ruleBuilder)
            => ruleBuilder.SetValidator(new IsInAdminTenantContextValidator());

        public static IRuleBuilderOptions<T, AdminContextData> IsValid<T>(
            this IRuleBuilder<T, AdminContextData> ruleBuilder)
        {
            throw new System.NotImplementedException();
        }

        public static IRuleBuilderOptions<T, TProperty> WithMessageCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ValidationErrorCode code)
            => rule.WithMessage(code.ToString());
    }
}