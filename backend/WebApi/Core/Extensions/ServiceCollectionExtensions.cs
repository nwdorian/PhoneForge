using System.Reflection;
using Application.Core;
using Asp.Versioning;
using Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApi.Core.Infrastructure;

namespace WebApi.Core.Extensions;

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
        services.AddInfrastructure(configuration);

        services.AddApiVersioning();

        services.AddCustomExceptionHandler();

        services.AddCustomProblemDetails();

        services.AddEndpoints(Assembly.GetExecutingAssembly());

        return services;
    }

    private static void AddPresentation(this IServiceCollection services)
    {
        services.AddOpenApi();
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

    private static void AddCustomExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }

    private static void AddCustomProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(configure =>
        {
            configure.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.TryAdd(
                    "requestId",
                    context.HttpContext.TraceIdentifier
                );
            };
        });
    }
}
