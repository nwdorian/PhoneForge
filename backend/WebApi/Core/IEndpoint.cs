namespace WebApi.Core;

/// <summary>
/// Represents a marker interface for types that define API endpoints.
/// Implementing this interface allows the endpoint to be automatically
/// discovered and registered in the application's routing pipeline.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Maps the endpoint to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <param name="app">The route builder used to configure the endpoint.</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}
