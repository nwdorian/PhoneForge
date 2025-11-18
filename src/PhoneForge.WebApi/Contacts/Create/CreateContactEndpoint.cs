using PhoneForge.UseCases.Contacts.Create;
using PhoneForge.WebApi.Core.Extensions;
using PhoneForge.WebApi.Core.Infrastructure;
using PhoneForge.WebApi.Endpoints;

namespace PhoneForge.WebApi.Contacts.Create;

internal sealed class CreateContactEndpoint : IEndpoint
{
    public const string Name = "CreateContact";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Contacts.Create, Handler)
            .WithName(Name)
            .WithTags(Tags.Contacts)
            .MapToApiVersion(1)
            .WithRequestValidation<CreateContactRequest>();
    }

    private static async Task<IResult> Handler(
        CreateContactRequest request,
        CreateContact useCase,
        CancellationToken cancellationToken
    )
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
}
