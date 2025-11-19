using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApi.Core.Middleware;

namespace WebApi.Core.Extensions;

/// <summary>
/// Provides extension methods for registering application middleware.
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Registers the web application middleware.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>The same <see cref="WebApplication"/> instance, allowing for method chaning.</returns>
    public static async Task<IApplicationBuilder> UseWebApplicationMiddleware(
        this WebApplication app
    )
    {
        app.MapEndpoints();

        app.UseOpenApi();
        app.UseHttpsRedirection();

        app.UseRequestContextLogging();
        app.UseSerilogRequestLogging();

        app.UseExceptionHandler();

        await app.ApplyMigrations();

        return app;
    }

    private static void UseOpenApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
    }

    private static void UseRequestContextLogging(this WebApplication app)
    {
        app.UseMiddleware<RequestLogContextMiddleware>();
    }

    private static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext =
            scope.ServiceProvider.GetRequiredService<PhoneForgeDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
