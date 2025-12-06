using WebApi.Contacts.Get;
using Xunit;

namespace TestData.Contacts.Get;

public class GetContactsValidData : TheoryData<GetContactsRequest, int, int, bool, bool>
{
    public GetContactsValidData()
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
