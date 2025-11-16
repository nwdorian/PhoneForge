using Asp.Versioning;
using FluentValidation;
using PhoneForge.Infrastructure;
using PhoneForge.Persistence;
using PhoneForge.UseCases;

namespace PhoneForge.WebApi.Extensions;

/// <summary>
/// Provides extension methods for registering application services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the web application services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, allowing for method chaining.</returns>
    public static IServiceCollection AddWebApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddPresentation();
        services.AddUseCases();
        services.AddInfrastructure();
        services.AddPersistence(configuration);

        services.AddFluentValidation();

        services.AddApiVersioning();

        return services;
    }

    private static void AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }

    private static void AddApiVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}
