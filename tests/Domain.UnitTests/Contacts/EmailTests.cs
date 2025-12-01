using Domain.Contacts;
using Domain.Core.Primitives;

namespace Domain.UnitTests.Contacts;

public class EmailTests
{
    [Fact]
    public void Create_Should_ReturnSuccess_WithValidInput()
    {
        string input = "jdoe@gmail.com";

        Result<Email> result = Email.Create(input);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(input, result.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Should_ReturnError_WithNullOrEmptyInput(string? input)
    {
        Result<Email> result = Email.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.Email.NullOrEmpty, result.Error);
    }

    [Fact]
    public void Create_Should_ReturnError_WithTooLongInput()
    {
        string input = new('a', Email.MaxLength + 1);

        Result<Email> result = Email.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.Email.LongerThanAllowed, result.Error);
    }

    [Theory]
    [InlineData("jdoe")]
    [InlineData("jdoegmail.com")]
    public void Create_Should_ReturnError_WithInvalidFormatInput(string? input)
    {
        Result<Email> result = Email.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.Email.InvalidFormat, result.Error);
    }
}
