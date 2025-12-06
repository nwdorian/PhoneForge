using WebApi.Contacts.Get;

namespace TestData.Contacts.Get;

public static class GetContactsRequestData
{
    public static GetContactsRequest CreateDefaultValidRequest()
    {
        return new GetContactsRequest(null);
    }

    public static GetContactsRequest CreateValidRequestWithSearchTerm()
    {
        return new GetContactsRequest(EmailData.ValidEmail);
    }

    public static GetContactsRequest CreateRequestWithInvalidPage()
    {
        return new GetContactsRequest(null, Page: 0);
    }

    public static GetContactsRequest CreateRequestWithInvalidPageSize()
    {
        return new GetContactsRequest(null, PageSize: 40);
    }
}
