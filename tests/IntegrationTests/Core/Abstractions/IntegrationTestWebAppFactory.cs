using Infrastructure.Database;

[assembly: ExcludeFromCodeCoverage]

namespace IntegrationTests.Core.Abstractions;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(
            (context, services) =>
            {
                services.RemoveAll<DbContextOptions<PhoneForgeDbContext>>();

                string? connectionString = context.Configuration.GetConnectionString(
                    "PhoneForgeTests"
                );

                services.AddSqlServer<PhoneForgeDbContext>(connectionString);
            }
        );
    }
}
