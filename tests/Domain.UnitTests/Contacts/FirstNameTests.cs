using Domain.Contacts;
using Domain.Core.Primitives;
using TestData.FirstNames.Cases;

namespace UnitTests.Contacts;

public class FirstNameTests
{
    [Theory]
    [ClassData(typeof(FirstNameValid))]
    public void Create_Should_ReturnSuccess_WithValidInput(string input)
    {
        Result<FirstName> result = FirstName.Create(input);

        Assert.True(result.IsSuccess);
        Assert.Equal(input, result.Value);
    }

    [Theory]
    [ClassData(typeof(FirstNameInvalid))]
    public void Create_Should_ReturnError_WithInvalidInput(string? input, Error expected)
    {
        Result<FirstName> result = FirstName.Create(input);

        Assert.True(result.IsFailure);
        Assert.Equal(expected, result.Error);
    }
}
