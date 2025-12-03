using Application.Contacts.Create;
using Bogus;
using Domain.Contacts;
using Domain.Core.Primitives;

namespace IntegrationTests.TestData.Contacts;

public class CreateContactInvalidData : TheoryData<CreateContactCommand, Error>
{
    private readonly Faker _faker = new();

    public CreateContactInvalidData()
    {
        Add(
            new CreateContactCommand(
                string.Empty,
                _faker.Name.LastName(),
                _faker.Internet.Email(),
                $"09{_faker.Random.Int(100000, 9999999)}"
            ),
            ContactErrors.FirstName.NullOrEmpty
        );

        Add(
            new CreateContactCommand(
                _faker.Name.FirstName(),
                string.Empty,
                _faker.Internet.Email(),
                $"09{_faker.Random.Int(100000, 9999999)}"
            ),
            ContactErrors.LastName.NullOrEmpty
        );

        Add(
            new CreateContactCommand(
                _faker.Name.FirstName(),
                _faker.Name.LastName(),
                string.Empty,
                $"09{_faker.Random.Int(100000, 9999999)}"
            ),
            ContactErrors.Email.NullOrEmpty
        );

        Add(
            new CreateContactCommand(
                _faker.Name.FirstName(),
                _faker.Name.LastName(),
                _faker.Internet.Email(),
                string.Empty
            ),
            ContactErrors.PhoneNumber.NullOrEmpty
        );
    }
}
