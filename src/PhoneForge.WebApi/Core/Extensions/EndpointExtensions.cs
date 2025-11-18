using System.Reflection;
using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhoneForge.WebApi.Core.Infrastructure;
using PhoneForge.WebApi.Endpoints;

namespace PhoneForge.WebApi.Core.Extensions;

internal static class EndpointExtensions
{
    internal static IApplicationBuilder MapEndpoints(this WebApplication app)
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

    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        var serviceDescriptors = assembly
            .DefinedTypes.Where(type =>
                type is { IsAbstract: false, IsInterface: false }
                && type.IsAssignableTo(typeof(IEndpoint))
            )
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static RouteHandlerBuilder WithRequestValidation<TRequest>(
        this RouteHandlerBuilder app
    )
    {
        return app.AddEndpointFilter<ValidationFilter<TRequest>>()
            .ProducesValidationProblem();
    }
}
