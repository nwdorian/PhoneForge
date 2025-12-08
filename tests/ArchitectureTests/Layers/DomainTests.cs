using NetArchTest.Rules;

namespace ArchitectureTests.Layers;

public class DomainTests : BaseTest
{
    [Fact]
    public void Domain_Should_NotHaveDependencyOn_Application()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Domain_Should_NotHaveDependencyOn_Infrastructure()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Domain_Should_NotHaveDependencyOn_WebApi()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(WebApiAssembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
