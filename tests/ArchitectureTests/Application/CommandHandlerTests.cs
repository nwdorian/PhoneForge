using Application.Core.Abstractions.Messaging;
using NetArchTest.Rules;

namespace ArchitectureTests.Application;

public class CommandHandlerTests : BaseTest
{
    [Fact]
    public void CommandHandler_Should_NotBePublic()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .NotBePublic()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void CommandHandler_Should_BeSealed()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
