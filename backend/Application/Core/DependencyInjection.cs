using Application.Core.Abstractions.Behaviors;
using Application.Core.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Core;

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
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddUseCases();
        services.AddDecorators();

        return services;
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(
                    classes => classes.AssignableTo(typeof(IQueryHandler<,>)),
                    publicOnly: false
                )
                .AsSelf()
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(
                    classes => classes.AssignableTo(typeof(ICommandHandler<>)),
                    publicOnly: false
                )
                .AsSelf()
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(
                    classes => classes.AssignableTo(typeof(ICommandHandler<,>)),
                    publicOnly: false
                )
                .AsSelf()
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
    }

    private static void AddDecorators(this IServiceCollection services)
    {
        services.Decorate(
            typeof(IQueryHandler<,>),
            typeof(LoggingDecorator.QueryHandler<,>)
        );
        services.Decorate(
            typeof(ICommandHandler<,>),
            typeof(LoggingDecorator.CommandHandler<,>)
        );
        services.Decorate(
            typeof(ICommandHandler<>),
            typeof(LoggingDecorator.CommandBaseHandler<>)
        );
    }
}
