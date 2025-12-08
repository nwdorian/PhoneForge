using TestData.Contacts.Requests;
using WebApi.Contacts.Update;

namespace IntegrationTests.Contacts.Cases.Update;

public class UpdateContactValidData : TheoryData<UpdateContactRequest>
{
    public UpdateContactValidData()
    {
        Add(UpdateContactRequestData.CreateValidRequest());
    }
}
