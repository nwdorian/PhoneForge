using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        _context = _factory
            .Services.CreateScope()
            .ServiceProvider.GetRequiredService<PhoneForgeDbContext>();

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
        PhoneForgeDbContext context = _factory
            .Services.CreateScope()
            .ServiceProvider.GetRequiredService<PhoneForgeDbContext>();

        return context;
    }
}
