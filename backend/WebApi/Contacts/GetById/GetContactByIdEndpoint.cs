using Application.Contacts;
using Application.Contacts.GetById;
using WebApi.Core;
using WebApi.Core.Extensions;
using WebApi.Core.Infrastructure;

namespace WebApi.Contacts.GetById;

internal sealed class GetContactByIdEndpoint : IEndpoint
{
    public const string Name = "GetContactById";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Contacts.GetById, Handler)
            .WithNameAndTags(Name, Tags.Contacts)
            .Produces<ContactResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .MapToApiVersion(1);
    }

    private static async Task<IResult> Handler(
        Guid contactId,
        GetContactById useCase,
        CancellationToken cancellationToken
    )
    {
        var query = new GetContactByIdQuery(contactId);

        var result = await useCase.Handle(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
