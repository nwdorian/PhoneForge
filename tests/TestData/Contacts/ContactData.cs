namespace TestData.Contacts;

public static class ContactData
{
    public static Contact ValidContact =>
        Contact.Create(
            FirstNameData.ValidFirstName,
            LastNameData.ValidLastName,
            EmailData.ValidEmail,
            PhoneNumberData.ValidPhoneNumber
        );
}
