using TestData.Contacts.Requests;
using WebApi.Contacts.Update;

namespace IntegrationTests.Contacts.Cases.Update;

public class UpdateContactInvalid : TheoryData<UpdateContactRequest, Error>
{
    public UpdateContactInvalid()
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
