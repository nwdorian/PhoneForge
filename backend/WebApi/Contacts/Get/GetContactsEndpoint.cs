using Application.Contacts.Get;
using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives;
using WebApi.Core;
using WebApi.Core.Constants;
using WebApi.Core.Extensions;
using WebApi.Core.Infrastructure;

namespace WebApi.Contacts.Get;

internal sealed class GetContactsEndpoint : IEndpoint
{
    public const string Name = "GetContacts";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Contacts.Get, Handler)
            .WithNameAndTags(Name, Tags.Contacts)
            .Produces<GetContactsResponse>(StatusCodes.Status200OK)
            .MapToApiVersion(1);
    }

    private static async Task<IResult> Handler(
        [AsParameters] GetContactsRequest request,
        IQueryHandler<GetContactsQuery, GetContactsResponse> useCase,
        CancellationToken cancellationToken
    )
    {
        GetContactsQuery query = new(
            request.SearchTerm,
            request.Page,
            request.PageSize,
            request.SortColumn,
            request.SortOrder
        );

        Result<GetContactsResponse> result = await useCase.Handle(
            query,
            cancellationToken
        );

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
