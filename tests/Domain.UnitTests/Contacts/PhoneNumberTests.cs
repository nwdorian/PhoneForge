using Domain.Contacts;
using Domain.Core.Primitives;
using TestData.PhoneNumbers.Cases;

namespace UnitTests.Contacts;

public class PhoneNumberTests
{
    [Theory]
    [ClassData(typeof(PhoneNumberValid))]
    public void Create_Should_ReturnSuccess_WithValidInput(string input)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(input);

        Assert.True(result.IsSuccess);
        Assert.Equal(input, result.Value);
    }

    [Theory]
    [ClassData(typeof(PhoneNumberInvalid))]
    public void Create_Should_ReturnError_WithInvalidInput(string? input, Error expected)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(input);

        Assert.True(result.IsFailure);
        Assert.Equal(expected, result.Error);
    }
}
