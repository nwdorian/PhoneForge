using Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTests;

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

            PhoneForgeDbContext dbContext = CreateDbContext(services);

            dbContext.Database.EnsureDeleted();
        });
    }

    private static PhoneForgeDbContext CreateDbContext(IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IServiceScope scope = serviceProvider.CreateScope();

        PhoneForgeDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<PhoneForgeDbContext>();
        return dbContext;
    }
}
