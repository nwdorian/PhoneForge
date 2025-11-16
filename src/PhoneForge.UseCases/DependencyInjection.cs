using Microsoft.Extensions.DependencyInjection;
using PhoneForge.UseCases.Contacts.Create;

namespace PhoneForge.UseCases;

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
        services.AddScoped<CreateContact>();

        return services;
    }
}
