using Domain.Contacts;

namespace TestData.LastNames;

public static class LastNameData
{
    public static readonly LastName ValidLastName = LastName
        .Create(nameof(LastName))
        .Value;

    public static readonly string LongerThanAllowedLastName = new(
        '*',
        LastName.MaxLength + 1
    );
}
