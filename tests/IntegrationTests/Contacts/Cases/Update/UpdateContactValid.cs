using TestData.Contacts.Requests;
using WebApi.Contacts.Update;

namespace IntegrationTests.Contacts.Cases.Update;

public class UpdateContactValid : TheoryData<UpdateContactRequest>
{
    public UpdateContactValid()
    {
        Add(UpdateContactRequestData.CreateValidRequest());
    }
}
