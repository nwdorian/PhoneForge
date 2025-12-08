using System.Diagnostics.CodeAnalysis;
using Infrastructure.Database;

[assembly: ExcludeFromCodeCoverage]

namespace IntegrationTests.Core.Abstractions;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>
{
    public IConfiguration? Configuration { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("integrationsettings.json")
                .Build();

            config.AddConfiguration(Configuration);
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<PhoneForgeDbContext>>();

            string? connectionString = Configuration?.GetConnectionString(
                "PhoneForgeTests"
            );

            services.AddSqlServer<PhoneForgeDbContext>(connectionString);
        });
    }
}
