using Domain.Core.Pagination;
using Domain.Core.Primitives;
using TestData.Contacts.Requests;
using WebApi.Contacts.Get;

namespace IntegrationTests.Contacts.Cases.Get;

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
