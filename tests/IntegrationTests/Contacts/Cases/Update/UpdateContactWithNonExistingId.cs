using TestData.Contacts.Requests;
using WebApi.Contacts.Update;

namespace IntegrationTests.Contacts.Cases.Update;

public class UpdateContactWithNonExistingId
    : TheoryData<UpdateContactRequest, Guid, Error>
{
    private readonly Guid _id = Guid.NewGuid();

    public UpdateContactWithNonExistingId()
    {
        Add(
            UpdateContactRequestData.CreateValidRequest(),
            _id,
            ContactErrors.NotFoundById(_id)
        );
    }
}
