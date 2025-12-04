using Application.Contacts.Create;

namespace IntegrationTests.TestData.Contacts;

public class CreateContactValidData : TheoryData<CreateContactCommand>
{
    public CreateContactValidData()
    {
        Add(
            new CreateContactCommand(
                ContactTestData.FirstName,
                ContactTestData.LastName,
                ContactTestData.Email,
                ContactTestData.PhoneNumber
            )
        );
    }
}
