using FluentValidation;
using NetArchTest.Rules;

namespace ArchitectureTests.WebApi;

public class ValidatorTests : BaseTest
{
    [Fact]
    public void Validator_ShouldHave_NameEndingWith_Validator()
    {
        TestResult result = Types
            .InAssembly(WebApiAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Validator_Should_NotBePublic()
    {
        TestResult result = Types
            .InAssembly(WebApiAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .NotBePublic()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Validator_Should_BeSealed()
    {
        TestResult result = Types
            .InAssembly(WebApiAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
