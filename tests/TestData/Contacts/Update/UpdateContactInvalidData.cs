using Domain.Contacts;
using Domain.Core.Primitives;
using WebApi.Contacts.Update;
using Xunit;

namespace TestData.Contacts.Update;

public class UpdateContactInvalidData : TheoryData<UpdateContactRequest, Error>
{
    public UpdateContactInvalidData()
    {
        Add(
            UpdateContactRequestData.CreateRequestWithInvalidFirstName(),
            ContactErrors.FirstName.LongerThanAllowed
        );

        Add(
            UpdateContactRequestData.CreateRequestWithInvalidLastName(),
            ContactErrors.LastName.LongerThanAllowed
        );

        Add(
            UpdateContactRequestData.CreateRequestWithInvalidEmail(),
            ContactErrors.Email.LongerThanAllowed
        );

        Add(
            UpdateContactRequestData.CreateRequestWithInvalidPhoneNumber(),
            ContactErrors.PhoneNumber.LongerThanAllowed
        );
    }
}
