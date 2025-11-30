using Domain.Contacts;
using Domain.Core.Primitives;

namespace UnitTests.Domain.Contacts;

public class PhoneNumberTests
{
    [Fact]
    public void Create_Should_ReturnSuccess_WithValidInput()
    {
        string input = "0919876543";

        Result<PhoneNumber> result = PhoneNumber.Create(input);

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
        Result<PhoneNumber> result = PhoneNumber.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.PhoneNumber.NullOrEmpty, result.Error);
    }

    [Fact]
    public void Create_Should_ReturnError_WithTooLongInput()
    {
        string input = new('a', PhoneNumber.MaxLength + 1);

        Result<PhoneNumber> result = PhoneNumber.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.PhoneNumber.LongerThanAllowed, result.Error);
    }

    [Fact]
    public void Create_Should_ReturnError_WithTooShortInput()
    {
        string input = new('a', PhoneNumber.MinLength - 1);

        Result<PhoneNumber> result = PhoneNumber.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.PhoneNumber.ShorterThanAllowed, result.Error);
    }

    [Fact]
    public void Create_Should_ReturnError_WithInvalidFormatInput()
    {
        string input = "091987654a";

        Result<PhoneNumber> result = PhoneNumber.Create(input);

        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(ContactErrors.PhoneNumber.InvalidFormat, result.Error);
    }
}
