using Domain.Contacts;
using Domain.Core.Primitives;
using WebApi.Contacts.Update;
using Xunit;

namespace TestData.Contacts.Update;

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
