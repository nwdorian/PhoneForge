using Domain.Contacts;
using Domain.Core.Primitives;
using TestData.Contacts.Requests;
using WebApi.Contacts.Update;

namespace IntegrationTests.Contacts.Cases.Update;

public class UpdateContactWithNonExistingIdData
    : TheoryData<UpdateContactRequest, Guid, Error>
{
    private readonly Guid _id = Guid.NewGuid();

    public UpdateContactWithNonExistingIdData()
    {
        Add(
            UpdateContactRequestData.CreateValidRequest(),
            _id,
            ContactErrors.NotFoundById(_id)
        );
    }
}
