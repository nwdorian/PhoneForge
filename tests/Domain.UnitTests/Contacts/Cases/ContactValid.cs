using Domain.Contacts;
using TestData.Contacts;

namespace UnitTests.Contacts.Cases;

public class ContactValid : TheoryData<FirstName, LastName, Email, PhoneNumber, string>
{
    public ContactValid()
    {
        Add(
            FirstNameData.ValidFirstName,
            LastNameData.ValidLastName,
            EmailData.ValidEmail,
            PhoneNumberData.ValidPhoneNumber,
            $"{FirstNameData.ValidFirstName.Value} {LastNameData.ValidLastName.Value}"
        );
    }
}
