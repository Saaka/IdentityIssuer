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
        {
            return ruleBuilder
                .SetValidator(new RequestHasUserContextValidator());
        }

        public static IRuleBuilderOptions<T, RequestContextData> IsInTenantContext<T>(
            this IRuleBuilder<T, RequestContextData> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new IsInTenantContextValidator());
        }

        public static IRuleBuilderOptions<T, RequestContextData> IsInAdminTenantContext<T>(
            this IRuleBuilder<T, RequestContextData> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new IsInAdminTenantContextValidator());
        }
        
        public static IRuleBuilderOptions<T, AdminContextData> IsValid<T>(
            this IRuleBuilder<T, AdminContextData> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new AdminContextDataValidator());
        }

        public static IRuleBuilderOptions<T, TProperty> WithMessageCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ValidationErrorCode code)
        {
            return rule
                .WithMessage(code.ToString());
        }
    }
}