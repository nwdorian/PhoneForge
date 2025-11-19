using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneForge.UseCases.Abstractions.Data;

namespace PhoneForge.Persistence;

/// <summary>
/// Provides extension methods for registering persistence services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the persistence layer services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, allowing for method chaining.</returns>
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("PhoneForgeDb");
        services.AddDbContext<PhoneForgeDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services.AddScoped<IDbContext, PhoneForgeDbContext>();

        return services;
    }
}
