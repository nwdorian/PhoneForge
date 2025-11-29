using Application.Contacts;
using Application.Contacts.Get;
using Application.Core.Abstractions.Messaging;
using Domain.Core.Pagination;
using Domain.Core.Primitives;
using WebApi.Core;
using WebApi.Core.Constants;
using WebApi.Core.Extensions;
using WebApi.Core.Infrastructure;

namespace WebApi.Contacts.Get;

internal sealed class GetContactsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Contacts.Get, Handler);
    }

    private static async Task<IResult> Handler(
        [AsParameters] GetContactsRequest request,
        IQueryHandler<GetContactsQuery, PagedList<ContactResponse>> useCase,
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

        Result<PagedList<ContactResponse>> result = await useCase.Handle(
            query,
            cancellationToken
        );

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
