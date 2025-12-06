using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using TestData.Seeding;

namespace IntegrationTests.Core.Abstractions;

[Collection("IntegrationTests")]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly PhoneForgeDbContext DbContext;
    protected readonly TestDataSeeder DataSeeder;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider.GetRequiredService<PhoneForgeDbContext>();
        DataSeeder = new TestDataSeeder(DbContext);
        HttpClient = factory.CreateClient();
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
