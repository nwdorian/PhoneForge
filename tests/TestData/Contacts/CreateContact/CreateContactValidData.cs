using WebApi.Contacts.Create;
using Xunit;

namespace TestData.Contacts.CreateContact;

public class CreateContactValidData : TheoryData<CreateContactRequest>
{
    public CreateContactValidData()
    {
        Add(CreateContactRequestData.CreateValidRequest());
    }
}
