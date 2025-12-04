using Domain.Contacts;

namespace IntegrationTests.TestData.Contacts;

public static class ContactTestData
{
    public static readonly FirstName FirstName = FirstName.Create("first").Value;

    public static readonly LastName LastName = LastName.Create("last").Value;

    public static readonly Email Email = Email.Create("test@phoneforge.com").Value;

    public static readonly PhoneNumber PhoneNumber = PhoneNumber
        .Create("091234567")
        .Value;

    public static Contact ValidContact =>
        Contact.Create(FirstName, LastName, Email, PhoneNumber);
}
