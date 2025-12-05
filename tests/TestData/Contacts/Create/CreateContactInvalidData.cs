using Domain.Contacts;
using Domain.Core.Primitives;
using WebApi.Contacts.Create;
using Xunit;

namespace TestData.Contacts.Create;

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
