using PhoneForge.WebApi.Endpoints.V1.Contacts.Create;

namespace PhoneForge.WebApi.Endpoints.V1.Contacts;

/// <summary>
/// Provides extension method for mapping contact-related API endpoints.
/// </summary>
public static class ContactsEndpointsExtensions
{
    /// <summary>
    /// Maps all contact-related endpoints to the application's routing pipeline.
    /// </summary>
    /// <param name="app">The route builder used to configure the endpoints.</param>
    public static void MapContactsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateContact();
    }
}
