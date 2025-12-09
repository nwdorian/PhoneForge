using TestData.Contacts.Requests;
using WebApi.Contacts.Create;

namespace IntegrationTests.Contacts.Cases.Create;

public class CreateContactInvalid : TheoryData<CreateContactRequest, Error>
{
    public CreateContactInvalid()
    {
        Add(
            CreateContactRequestData.CreateRequestWithInvalidFirstName(),
            ContactErrors.FirstName.LongerThanAllowed
        );

        Add(
            CreateContactRequestData.CreateRequestWithInvalidLastName(),
            ContactErrors.LastName.LongerThanAllowed
        );

        Add(
            CreateContactRequestData.CreateRequestWithInvalidEmail(),
            ContactErrors.Email.LongerThanAllowed
        );

        Add(
            CreateContactRequestData.CreateRequestWithInvalidPhoneNumber(),
            ContactErrors.PhoneNumber.LongerThanAllowed
        );
    }
}
