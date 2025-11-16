using PhoneForge.UseCases.Contacts.Create;
using PhoneForge.WebApi.Extensions;
using PhoneForge.WebApi.Infrastructure;

namespace PhoneForge.WebApi.Endpoints.V1.Contacts.Create;

/// <summary>
/// Provides endpoint mappings related to creating contacts.
/// </summary>
public static class CreateContactEndpoint
{
    /// <summary>
    /// The name of the endpoint used for creating a new contact.
    /// </summary>
    public const string Name = "CreateContact";

    /// <summary>
    /// Maps the endpoint responsible for creating a new contact.
    /// </summary>
    /// <param name="app">The route builder used to configure the endpoint.</param>
    public static void MapCreateContact(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            Routes.Contacts.Create,
            async (
                CreateContactRequest request,
                CreateContact useCase,
                CancellationToken cancellationToken
            ) =>
            {
                var command = new CreateContactCommand(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.PhoneNumber
                );

                var result = await useCase.Handle(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            }
        );
    }
}
