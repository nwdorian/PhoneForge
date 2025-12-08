using TestData.Contacts.Requests;
using WebApi.Contacts.Create;

namespace IntegrationTests.Contacts.Cases.Create;

public class CreateContactValidData : TheoryData<CreateContactRequest>
{
    public CreateContactValidData()
    {
        Add(CreateContactRequestData.CreateValidRequest());
    }
}
