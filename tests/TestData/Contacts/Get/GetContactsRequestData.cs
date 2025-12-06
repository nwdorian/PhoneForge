using Domain.Core.Pagination;
using WebApi.Contacts.Get;

namespace TestData.Contacts.Get;

public static class GetContactsRequestData
{
    public static GetContactsRequest CreateDefaultValidRequest()
    {
        return new GetContactsRequest(null);
    }

    public static GetContactsRequest CreateValidRequestWithSecondPage()
    {
        return new GetContactsRequest(null, 2);
    }

    public static GetContactsRequest CreateRequestWithInvalidPage()
    {
        return new GetContactsRequest(null, Page: 0);
    }

    public static GetContactsRequest CreateRequestWithGreaterThanAllowedPageSize()
    {
        return new GetContactsRequest(null, PageSize: PageSize.MaximumPageSize + 1);
    }

    public static GetContactsRequest CreateRequestWithLowerThanAllowedPageSize()
    {
        return new GetContactsRequest(null, PageSize: PageSize.MinimumPageSize - 1);
    }
}
