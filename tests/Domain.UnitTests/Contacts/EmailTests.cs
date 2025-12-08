using Domain.Contacts;
using Domain.Core.Primitives;
using TestData.Emails.Cases;

namespace UnitTests.Contacts;

public class EmailTests
{
    [Theory]
    [ClassData(typeof(EmailValid))]
    public void Create_Should_ReturnSuccess_WithValidInput(string input)
    {
        Result<Email> result = Email.Create(input);

        Assert.True(result.IsSuccess);
        Assert.Equal(input, result.Value);
    }

    [Theory]
    [ClassData(typeof(EmailInvalid))]
    public void Create_Should_ReturnError_WithInvalidInput(string? input, Error expected)
    {
        Result<Email> result = Email.Create(input);

        Assert.True(result.IsFailure);
        Assert.Equal(expected, result.Error);
    }
}
