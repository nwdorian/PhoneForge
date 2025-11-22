using System.Reflection;
using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApi.Core.Infrastructure;

namespace WebApi.Core.Extensions;

/// <summary>
/// Provides extension methods for registering and mapping API endpoints.
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    /// Maps all registered <see cref="IEndpoint"/> implementations to the application's routing pipeline,
    /// using API versioning.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> used to map the endpoints.</param>
    /// <returns>The same <see cref="IApplicationBuilder"/> instance for chaining.</returns>
    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        var builder = app.MapGroup("api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }

    /// <summary>
    /// Scans the specified assembly for all types implementing <see cref="IEndpoint"/>
    /// and registers them as transient services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add the endpoint services to.</param>
    /// <param name="assembly">The assembly to scan for endpoint implementations.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        var types = assembly
            .DefinedTypes.Where(type =>
                type is { IsAbstract: false, IsInterface: false }
                && type.IsAssignableTo(typeof(IEndpoint))
            )
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(types);

        return services;
    }

    /// <summary>
    /// Adds a request validation filter of type <typeparamref name="TRequest"/> to the route handler
    /// and configures the endpoint to produce validation problem responses.
    /// </summary>
    /// <typeparam name="TRequest">The type of request to validate.</typeparam>
    /// <param name="app">The route handler builder to configure.</param>
    /// <returns>The same <see cref="RouteHandlerBuilder"/> instance for chaining.</returns>
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(
        this RouteHandlerBuilder app
    )
    {
        return app.AddEndpointFilter<ValidationFilter<TRequest>>()
            .ProducesValidationProblem();
    }

    /// <summary>
    /// Adds a name and a tag to the endpoint metadata.
    /// </summary>
    /// <param name="app">The route handler builder to configure.</param>
    /// <param name="name">Name of the endpoint.</param>
    /// <param name="tags">Tag of the endpoint.</param>
    /// <returns></returns>
    public static RouteHandlerBuilder WithNameAndTags(
        this RouteHandlerBuilder app,
        string name,
        string tags
    )
    {
        return app.WithName(name).WithTags(tags);
    }
}
