using Infrastructure.Database;
using TestData.Seeding;

namespace IntegrationTests.Core.Abstractions;

[Collection("IntegrationTests")]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private readonly IntegrationTestWebAppFactory _factory;
    private readonly PhoneForgeDbContext _context;
    protected readonly TestDataSeeder DataSeeder;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;

        using IServiceScope scope = _factory.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<PhoneForgeDbContext>();

        DataSeeder = new TestDataSeeder(_context);
        HttpClient = factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        await DataSeeder.SeedAsync();
    }

    public async Task DisposeAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Contacts");
    }

    public PhoneForgeDbContext CreateDbContext()
    {
        using IServiceScope scope = _factory.Services.CreateScope();
        PhoneForgeDbContext context =
            scope.ServiceProvider.GetRequiredService<PhoneForgeDbContext>();

        return context;
    }
}
