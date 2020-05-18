using IdentityIssuer.Application.Validators.FluentValidation;
using Xunit;
using FluentValidation.TestHelper;
using IdentityIssuer.Common.Requests.RequestContext;

namespace IdentityIssuer.Application.UnitTests.Validators
{
    [Trait("Application", "Validators")]
    public class TenantContextValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void Should_Have_Validation_Error_For_Null_Or_Empty_Code(string code)
        {
            var sut = new TenantContextValidator();

            sut.ShouldHaveValidationErrorFor(x => x.TenantCode, 
                new TenantContext(1, code, false));
        }

        [Fact]
        public void Should_Have_Validation_Error_For_Empty_Id()
        {
            var sut = new TenantContextValidator();

            sut.ShouldHaveValidationErrorFor(x => x.TenantId, 
                new TenantContext(0, "TST", false));
        }

        [Fact]
        public void Should_Not_Have_Validation_Errors_For_Valid_Context_Data()
        {
            var sut = new TenantContextValidator();
            
            var data = new TenantContext(1, "TST", false);
            sut.ShouldNotHaveValidationErrorFor(x=> x.TenantCode, data);
            sut.ShouldNotHaveValidationErrorFor(x=> x.TenantId, data);
            sut.ShouldNotHaveValidationErrorFor(x=> x.IsAdminTenant, data);
        }
    }
}