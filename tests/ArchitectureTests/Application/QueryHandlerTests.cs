using Application.Core.Abstractions.Messaging;
using NetArchTest.Rules;

namespace ArchitectureTests.Application;

public class QueryHandlerTests : BaseTest
{
    [Fact]
    public void QueryHandler_Should_NotBePublic()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .NotBePublic()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void QueryHandler_Should_BeSealed()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void QueryHandler_ShouldHave_NameEndingWith_Handler()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .And()
            .AreNotGeneric()
            .Should()
            .HaveNameEndingWith("Handler")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
