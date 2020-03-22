using FluentValidation;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Validators.FluentValidation;
using IdentityIssuer.Common.Enums;

namespace IdentityIssuer.Application
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TenantContextData> IsValid<T>(
            this IRuleBuilder<T, TenantContextData> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new TenantContextDataValidator());
        }
        
        public static IRuleBuilderOptions<T, UserContextData> IsValid<T>(
            this IRuleBuilder<T, UserContextData> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new UserContextDataValidator());
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