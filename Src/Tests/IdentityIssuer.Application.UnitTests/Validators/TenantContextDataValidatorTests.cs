using IdentityIssuer.Application.Validators.FluentValidation;
using Xunit;
using FluentValidation.TestHelper;
using IdentityIssuer.Application.Models;

namespace IdentityIssuer.Application.UnitTests.Validators
{
    [Trait("Application", "Validators")]
    public class TenantContextDataValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void Should_Have_Validation_Error_For_Null_Or_Empty_Code(string code)
        {
            var sut = new TenantContextDataValidator();

            sut.ShouldHaveValidationErrorFor(x => x.TenantCode, new TenantContextData(1, code));
        }

        [Fact]
        public void Should_Have_Validation_Error_For_Empty_Id()
        {
            var sut = new TenantContextDataValidator();

            sut.ShouldHaveValidationErrorFor(x => x.TenantId, new TenantContextData(0, "TST"));
        }

        [Fact]
        public void Should_Not_Have_Validation_Errors_For_Valid_Context_Data()
        {
            var sut = new TenantContextDataValidator();
            
            var data = new TenantContextData(1, "TST");
            sut.ShouldNotHaveValidationErrorFor(x=> x.TenantCode, data);
            sut.ShouldNotHaveValidationErrorFor(x=> x.TenantId, data);
        }
    }
}