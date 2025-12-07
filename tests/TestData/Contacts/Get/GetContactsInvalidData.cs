using Domain.Core.Pagination;
using Domain.Core.Primitives;
using WebApi.Contacts.Get;
using Xunit;

namespace TestData.Contacts.Get;

public class GetContactsInvalidData : TheoryData<GetContactsRequest, Error>
{
    public GetContactsInvalidData()
    {
        Add(
            GetContactsRequestData.CreateRequestWithInvalidPage(),
            PaginationErrors.InvalidPage
        );
        Add(
            GetContactsRequestData.CreateRequestWithGreaterThanAllowedPageSize(),
            PaginationErrors.GreaterThanAllowed
        );
        Add(
            GetContactsRequestData.CreateRequestWithLowerThanAllowedPageSize(),
            PaginationErrors.LowerThanAllowed
        );
    }
}
