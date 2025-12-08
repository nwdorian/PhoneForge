using Domain.Contacts;
using Domain.Core.Primitives;
using TestData.Contacts.Requests;
using WebApi.Contacts.Create;
using Xunit;

namespace IntegrationTests.Contacts.Cases.Create;

public class CreateContactInvalidData : TheoryData<CreateContactRequest, Error>
{
    public CreateContactInvalidData()
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
