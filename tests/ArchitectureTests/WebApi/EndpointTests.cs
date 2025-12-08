using NetArchTest.Rules;
using WebApi.Core;

namespace ArchitectureTests.WebApi;

public class EndpointTests : BaseTest
{
    [Fact]
    public void Endpoint_ShouldHave_NameEndingWith_Endpoint()
    {
        TestResult result = Types
            .InAssembly(WebApiAssembly)
            .That()
            .Inherit(typeof(IEndpoint))
            .Should()
            .HaveNameEndingWith("Endpoint")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Endpoint_Should_NotBePublic()
    {
        TestResult result = Types
            .InAssembly(WebApiAssembly)
            .That()
            .ImplementInterface(typeof(IEndpoint))
            .Should()
            .NotBePublic()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Endpoint_Should_BeSealed()
    {
        TestResult result = Types
            .InAssembly(WebApiAssembly)
            .That()
            .ImplementInterface(typeof(IEndpoint))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
