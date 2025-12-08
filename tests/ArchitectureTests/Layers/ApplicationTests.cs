using NetArchTest.Rules;

namespace ArchitectureTests.Layers;

public class ApplicationTests : BaseTest
{
    [Fact]
    public void Application_ShouldNotHaveDependencyOn_Infrastructure()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Application_ShouldNotHaveDependencyOn_WebApi()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(WebApiAssembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
