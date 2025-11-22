using Application.Contacts.Create;
using WebApi.Core;
using WebApi.Core.Extensions;
using WebApi.Core.Infrastructure;

namespace WebApi.Contacts.Create;

internal sealed class CreateContactEndpoint : IEndpoint
{
    public const string Name = "CreateContact";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Contacts.Create, Handler)
            .WithNameAndTags(Name, Tags.Contacts)
            .WithRequestValidation<CreateContactRequest>()
            .MapToApiVersion(1);
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
