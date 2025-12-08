using Domain.Contacts;
using Domain.Core.Primitives;
using Xunit;

namespace TestData.FirstNames.Cases;

public class FirstNameInvalid : TheoryData<string?, Error>
{
    public FirstNameInvalid()
    {
        Add(null, ContactErrors.FirstName.NullOrEmpty);
        Add(string.Empty, ContactErrors.FirstName.NullOrEmpty);
        Add(" ", ContactErrors.FirstName.NullOrEmpty);
        Add(
            FirstNameData.LongerThanAllowedFirstName,
            ContactErrors.FirstName.LongerThanAllowed
        );
    }
}
