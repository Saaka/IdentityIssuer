using FluentValidation;
using IdentityIssuer.Application.Models;
using IdentityIssuer.Application.Validators.FluentValidation;

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
    }
}