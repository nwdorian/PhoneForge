using TestData.Contacts.Requests;
using WebApi.Contacts.Create;

namespace IntegrationTests.Contacts.Cases.Create;

public class CreateContactValid : TheoryData<CreateContactRequest>
{
    public CreateContactValid()
    {
        Add(CreateContactRequestData.CreateValidRequest());
    }
}
