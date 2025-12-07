using WebApi.Contacts.Update;
using Xunit;

namespace TestData.Contacts.Update;

public class UpdateContactValidData : TheoryData<UpdateContactRequest>
{
    public UpdateContactValidData()
    {
        Add(UpdateContactRequestData.CreateValidRequest());
    }
}
