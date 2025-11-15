using Microsoft.Extensions.DependencyInjection;
using PhoneForge.Infrastructure.Time;
using SharedKernel;

namespace PhoneForge.Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the infrastructure layer services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>
    /// The same <see cref="IServiceCollection"/> instance, allowing for method chaining.
    /// </returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
