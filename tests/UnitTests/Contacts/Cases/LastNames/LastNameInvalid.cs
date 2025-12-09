using TestData.Contacts;

namespace UnitTests.Contacts.Cases.LastNames;

public class LastNameInvalid : TheoryData<string?, Error>
{
    public LastNameInvalid()
    {
        Add(null, ContactErrors.LastName.NullOrEmpty);
        Add(string.Empty, ContactErrors.LastName.NullOrEmpty);
        Add(" ", ContactErrors.LastName.NullOrEmpty);
        Add(
            LastNameData.LongerThanAllowedLastName,
            ContactErrors.LastName.LongerThanAllowed
        );
    }
}
