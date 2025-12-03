using Application.Contacts;
using Application.Contacts.Create;
using Domain.Contacts;
using Domain.Core.Primitives;
using IntegrationTests.TestData.Contacts;

namespace IntegrationTests.Contacts;

public class CreateContactTests : BaseIntegrationTest
{
    private readonly CreateContact _useCase;

    public CreateContactTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        _useCase = GetUseCase<CreateContact>();
    }

    [Fact]
    public async Task Handle_Should_AddNewContactToDatabase()
    {
        CreateContactCommand command = new(
            Faker.Name.FirstName(),
            Faker.Name.LastName(),
            Faker.Internet.Email(),
            $"09{Faker.Random.Int(1000000, 9999999)}"
        );

        Result<ContactResponse> result = await _useCase.Handle(
            command,
            CancellationToken.None
        );

        ContactResponse response = result.Value;

        Contact? contact = DbContext.Contacts.FirstOrDefault(c => c.Id == response.Id);

        Assert.True(result.IsSuccess);
        Assert.NotNull(contact);
    }

    [Theory]
    [ClassData(typeof(CreateContactInvalidData))]
    public async Task Handle_Should_ReturnError_WhenCommandIsInvalid(
        CreateContactCommand command,
        Error expected
    )
    {
        Result<ContactResponse> result = await _useCase.Handle(
            command,
            CancellationToken.None
        );

        Assert.True(result.IsFailure);
        Assert.Equal(expected, result.Error);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmailIsNotUnique()
    {
        string existingEmail = DataSeeder.GetTestContact().Email;

        CreateContactCommand command = new(
            Faker.Name.FirstName(),
            Faker.Name.LastName(),
            existingEmail,
            $"09{Faker.Random.Int(1000000, 9999999)}"
        );

        Result<ContactResponse> result = await _useCase.Handle(
            command,
            CancellationToken.None
        );

        Assert.True(result.IsFailure);
        Assert.Equal(ContactErrors.EmailNotUnique, result.Error);
    }
}
