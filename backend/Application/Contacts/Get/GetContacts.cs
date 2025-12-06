using System.Globalization;
using System.Linq.Expressions;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Contacts;
using Domain.Core.Pagination;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Application.Contacts.Get;

internal sealed class GetContacts(IDbContext context)
    : IQueryHandler<GetContactsQuery, GetContactsResponse>
{
    public async Task<Result<GetContactsResponse>> Handle(
        GetContactsQuery query,
        CancellationToken cancellationToken
    )
    {
        Result<Page> pageResult = Page.Create(query.Page);
        Result<PageSize> pageSizeResult = PageSize.Create(query.PageSize);

        Result firstFailOrSuccess = Result.FirstFailOrSuccess(pageResult, pageSizeResult);

        if (firstFailOrSuccess.IsFailure)
        {
            return firstFailOrSuccess.Error;
        }

        IQueryable<Contact> contactsQuery = context.Contacts;

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            contactsQuery = contactsQuery.Where(c =>
                c.FirstName.Value.Contains(query.SearchTerm)
                || c.LastName.Value.Contains(query.SearchTerm)
                || c.Email.Value.Contains(query.SearchTerm)
                || c.PhoneNumber.Value.Contains(query.SearchTerm)
            );
        }

        if (query.SortOrder?.ToLower(CultureInfo.InvariantCulture) == "desc")
        {
            contactsQuery = contactsQuery.OrderByDescending(GetSortProperty(query));
        }
        else
        {
            contactsQuery = contactsQuery.OrderBy(GetSortProperty(query));
        }

        int totalCount = await contactsQuery.CountAsync(cancellationToken);

        ContactResponse[] contactResponsesPage = await contactsQuery
            .Skip((pageResult.Value - 1) * pageSizeResult.Value)
            .Take(pageSizeResult.Value)
            .Select(c => new ContactResponse(
                c.Id,
                c.FirstName,
                c.LastName,
                c.FullName,
                c.Email,
                c.PhoneNumber,
                c.CreatedOnUtc
            ))
            .ToArrayAsync(cancellationToken);

        PagedList<ContactResponse> pagedList = new(
            contactResponsesPage,
            pageResult.Value,
            pageSizeResult.Value,
            totalCount
        );

        return new GetContactsResponse(
            pagedList.Page,
            pagedList.PageSize,
            pagedList.TotalCount,
            pagedList.TotalPages,
            pagedList.HasNextPage,
            pagedList.HasPreviousPage,
            pagedList.Items
        );
    }

    private static Expression<Func<Contact, object>> GetSortProperty(
        GetContactsQuery query
    )
    {
        return query.SortColumn.ToLower(CultureInfo.InvariantCulture) switch
        {
            "first_name" => contact => contact.FirstName.Value,
            "last_name" => contact => contact.LastName.Value,
            "email" => contact => contact.Email.Value,
            "phone_number" => contact => contact.PhoneNumber.Value,
            "created_on" => contact => contact.CreatedOnUtc,
            _ => contact => contact.CreatedOnUtc,
        };
    }
}
