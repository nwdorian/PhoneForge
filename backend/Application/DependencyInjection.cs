using System.Reflection;
using Application.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Provides extension methods for registering use case services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the use case layer services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, allowing for method chaining.</returns>
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddUseCases(typeof(IUseCase).Assembly);

        services.AddFluentValidation();

        return services;
    }

    private static void AddUseCases(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly
            .DefinedTypes.Where(type =>
                type is { IsAbstract: false, IsInterface: false }
                && type.IsAssignableTo(typeof(IUseCase))
            )
            .ToArray();

        foreach (var type in types)
        {
            services.AddScoped(type, type);
        }
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
