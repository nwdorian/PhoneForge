using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Core.Infrastructure;

/// <summary>
/// A global exception handler that captures unhandled exceptions and converts them
/// into standardized <see cref="ProblemDetails"/> responses.
/// </summary>
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _environment;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandler"/> class.
    /// </summary>
    /// <param name="problemDetailsService">
    /// The service responsible for writing <see cref="ProblemDetails"/> responses.
    /// </param>
    /// <param name="logger">The logger used to record exception details.</param>
    /// <param name="environment">Provides information about the current hosting environment.</param>
    public GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger,
        IHostEnvironment environment
    )
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
        _environment = environment;
    }

    /// <summary>
    /// Attempts to handle an unhandled exception by logging it and returning a standardized
    /// <see cref="ProblemDetails"/> response to the client.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="exception">The exception that occurred.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>
    /// <c>true</c> if the exception was successfully written as a problem details response;
    /// otherwise, <c>false</c>.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        _logger.LogError(exception, "Unhandled exception occurred");

        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = exception.GetType().Name,
                    Title = "An error occurred",
                    Detail = GetErrorDetail(exception),
                },
            }
        );
    }

    /// <summary>
    /// Returns an error message appropriate for the current hosting environment.
    /// In development, the exception message is returned. In production, a generic
    /// error message is used to avoid leaking internal details.
    /// </summary>
    /// <param name="exception">The exception to extract details from.</param>
    /// <returns>
    /// A detailed message in development, or a safe generic message in non-development environments.
    /// </returns>
    private string GetErrorDetail(Exception exception)
    {
        return _environment.IsDevelopment()
            ? exception.Message
            : "Something went wrong. Please try again later.";
    }
}
