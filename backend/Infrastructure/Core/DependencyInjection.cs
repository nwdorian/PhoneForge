using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Reports;
using Domain.Core.Abstractions;
using Infrastructure.Core.Time;
using Infrastructure.Database;
using Infrastructure.Documents;
using Infrastructure.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Core;

/// <summary>
/// Provides extension methods for registering infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the infrastructure layer services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>
    /// The same <see cref="IServiceCollection"/> instance, allowing for method chaining.
    /// </returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ExcelService>();
        services.AddScoped<IReportsService, ReportsService>();

        services.AddDatabase(configuration);

        return services;
    }

    private static void AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string? connectionString = configuration.GetConnectionString("PhoneForgeDb");
        services.AddDbContext<PhoneForgeDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services.AddScoped<IDbContext, PhoneForgeDbContext>();
    }
}
