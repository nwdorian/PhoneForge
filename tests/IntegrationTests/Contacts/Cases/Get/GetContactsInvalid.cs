using TestData.Contacts.Requests;
using WebApi.Contacts.Get;

namespace IntegrationTests.Contacts.Cases.Get;

public class GetContactsInvalid : TheoryData<GetContactsRequest, Error>
{
    public GetContactsInvalid()
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
