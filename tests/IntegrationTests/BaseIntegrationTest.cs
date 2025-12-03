using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly PhoneForgeDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider.GetRequiredService<PhoneForgeDbContext>();
    }

    protected T GetUseCase<T>()
        where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }
}
