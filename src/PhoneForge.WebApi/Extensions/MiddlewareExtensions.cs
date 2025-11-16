using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using PhoneForge.Persistence;
using PhoneForge.WebApi.Endpoints.V1.Contacts;

namespace PhoneForge.WebApi.Extensions;

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
        app.MapV1Endpoints();

        app.UseOpenApi();
        app.UseHttpsRedirection();

        await app.ApplyMigrations();

        return app;
    }

    private static void MapV1Endpoints(this WebApplication app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var versionedGroup = app.MapGroup("api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        versionedGroup.MapContactsEndpoints();
    }

    private static void UseOpenApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
    }

    private static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext =
            scope.ServiceProvider.GetRequiredService<PhoneForgeDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
