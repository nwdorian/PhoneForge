using NetArchTest.Rules;

namespace ArchitectureTests.Layers;

public class InfrastructureTests : BaseTest
{
    [Fact]
    public void Infrastructure_Should_NotHaveDependencyOn_WebApi()
    {
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(WebApiAssembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
