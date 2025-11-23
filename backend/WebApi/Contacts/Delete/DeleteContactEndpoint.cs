using Application.Contacts.Delete;
using Domain.Core.Primitives;
using WebApi.Core;
using WebApi.Core.Extensions;
using WebApi.Core.Infrastructure;

namespace WebApi.Contacts.Delete;

internal sealed class DeleteContactEndpoint : IEndpoint
{
    public const string Name = "DeleteContact";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Contacts.Delete, Handler)
            .WithNameAndTags(Name, Tags.Contacts)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .MapToApiVersion(1);
    }

    private static async Task<IResult> Handler(
        Guid contactId,
        DeleteContact useCase,
        CancellationToken cancellationToken
    )
    {
        DeleteContactCommand command = new(contactId);

        Result result = await useCase.Handle(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
