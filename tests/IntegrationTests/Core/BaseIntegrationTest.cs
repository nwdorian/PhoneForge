using Infrastructure.Database;
using IntegrationTests.TestData;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Core;

[Collection("IntegrationTests")]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly PhoneForgeDbContext DbContext;
    protected readonly TestDataSeeder DataSeeder;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider.GetRequiredService<PhoneForgeDbContext>();
        DataSeeder = new TestDataSeeder(DbContext);
    }

    protected T GetUseCase<T>()
        where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }

    public async Task InitializeAsync()
    {
        await DbContext.Database.EnsureCreatedAsync();
        await DataSeeder.SeedAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
    }
}
