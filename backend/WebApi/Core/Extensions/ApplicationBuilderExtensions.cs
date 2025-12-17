using System.Globalization;
using Serilog;
using Serilog.Formatting.Json;

namespace WebApi.Core.Extensions;

/// <summary>
/// Provides extension methods for configuring Serilog in the application.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures Serilog as the logging provider for the application using the settings
    /// specified in the application's configuration.
    /// </summary>
    /// <param name="builder">The application builder used to configure the host.</param>
    /// <returns>
    /// The same <see cref="WebApplicationBuilder"/> instance, allowing for method chaining.
    /// </returns>
    public static WebApplicationBuilder ConfigureSerilog(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.UseSerilog(
            (context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);

                config
                    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
                    .WriteTo.File(
                        new JsonFormatter(renderMessage: true),
                        "../../logs/log-.txt",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        shared: true
                    );

                config.Enrich.FromLogContext();
            }
        );

        return builder;
    }
}
