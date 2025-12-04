using Application.Contacts.Create;
using Domain.Contacts;
using Domain.Core.Primitives;

namespace IntegrationTests.TestData.Contacts;

public class CreateContactInvalidData : TheoryData<CreateContactCommand, Error>
{
    public CreateContactInvalidData()
    {
        Add(
            new CreateContactCommand(
                string.Empty,
                ContactTestData.LastName,
                ContactTestData.Email,
                ContactTestData.PhoneNumber
            ),
            ContactErrors.FirstName.NullOrEmpty
        );

        Add(
            new CreateContactCommand(
                ContactTestData.FirstName,
                string.Empty,
                ContactTestData.Email,
                ContactTestData.PhoneNumber
            ),
            ContactErrors.LastName.NullOrEmpty
        );

        Add(
            new CreateContactCommand(
                ContactTestData.FirstName,
                ContactTestData.LastName,
                string.Empty,
                ContactTestData.PhoneNumber
            ),
            ContactErrors.Email.NullOrEmpty
        );

        Add(
            new CreateContactCommand(
                ContactTestData.FirstName,
                ContactTestData.LastName,
                ContactTestData.Email,
                string.Empty
            ),
            ContactErrors.PhoneNumber.NullOrEmpty
        );
    }
}
