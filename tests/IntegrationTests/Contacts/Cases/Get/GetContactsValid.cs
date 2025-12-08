using TestData.Contacts.Requests;
using WebApi.Contacts.Get;

namespace IntegrationTests.Contacts.Cases.Get;

public class GetContactsValid : TheoryData<GetContactsRequest, int, int, bool, bool>
{
    public GetContactsValid()
    {
        Add(GetContactsRequestData.CreateDefaultValidRequest(), 20, 2, false, true);
        Add(
            GetContactsRequestData.CreateValidRequestWithSecondPage(),
            20,
            2,
            true,
            false
        );
    }
}
