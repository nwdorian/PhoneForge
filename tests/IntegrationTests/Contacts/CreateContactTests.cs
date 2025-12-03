using Application.Contacts;
using Application.Contacts.Create;
using Domain.Contacts;
using Domain.Core.Primitives;

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
        CreateContactCommand command = new("John", "Doe", "jdoe@gmail.com", "091234567");

        Result<ContactResponse> result = await _useCase.Handle(
            command,
            CancellationToken.None
        );

        ContactResponse response = result.Value;

        Contact? contact = DbContext.Contacts.FirstOrDefault(c => c.Id == response.Id);

        Assert.True(result.IsSuccess);
        Assert.NotNull(contact);
    }
}
