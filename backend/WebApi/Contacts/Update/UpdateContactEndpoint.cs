using Application.Contacts.Update;
using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives;
using WebApi.Core;
using WebApi.Core.Extensions;
using WebApi.Core.Infrastructure;

namespace WebApi.Contacts.Update;

internal sealed class UpdateContactEndpoint : IEndpoint
{
    public const string Name = "UpdateContact";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Contacts.Update, Handler)
            .WithNameAndTags(Name, Tags.Contacts)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .WithRequestValidation<UpdateContactRequest>()
            .MapToApiVersion(1);
    }

    private static async Task<IResult> Handler(
        Guid contactId,
        UpdateContactRequest request,
        ICommandHandler<UpdateContactCommand> useCase,
        CancellationToken cancellationToken
    )
    {
        UpdateContactCommand command = new(
            contactId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber
        );

        Result result = await useCase.Handle(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
