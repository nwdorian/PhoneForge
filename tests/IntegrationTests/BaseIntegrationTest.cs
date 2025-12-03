using Bogus;
using Infrastructure.Database;
using IntegrationTests.TestData;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public abstract class BaseIntegrationTest
    : IClassFixture<IntegrationTestWebAppFactory>,
        IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly PhoneForgeDbContext DbContext;
    protected readonly TestDataSeeder DataSeeder;
    protected readonly Faker Faker;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider.GetRequiredService<PhoneForgeDbContext>();
        DataSeeder = new TestDataSeeder(DbContext);
        Faker = new Faker();
    }

    protected T GetUseCase<T>()
        where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }

    public Task InitializeAsync()
    {
        return DataSeeder.SeedAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
