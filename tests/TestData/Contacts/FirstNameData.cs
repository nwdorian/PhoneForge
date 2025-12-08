using Domain.Contacts;

namespace TestData.Contacts;

public static class FirstNameData
{
    public static readonly FirstName ValidFirstName = FirstName
        .Create(nameof(FirstName))
        .Value;

    public static readonly string LongerThanAllowedFirstName = new(
        '*',
        FirstName.MaxLength + 1
    );
}
