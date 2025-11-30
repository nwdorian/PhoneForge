using Domain.Contacts;

namespace UnitTests.Contacts;

public class ContactUpdate
{
    [Fact]
    public void UpdateContact_ShouldChangeAllProperties()
    {
        FirstName firstName = FirstName.Create("John").Value;
        LastName lastName = LastName.Create("Doe").Value;
        Email email = Email.Create("jdoe@gmail.com").Value;
        PhoneNumber phoneNumber = PhoneNumber.Create("0919876543").Value;

        Contact contact = Contact.Create(firstName, lastName, email, phoneNumber);

        FirstName newFirstName = FirstName.Create("Ada").Value;
        LastName newLastName = LastName.Create("Lovelace").Value;
        Email newEmail = Email.Create("alovelace@gmail.com").Value;
        PhoneNumber newPhoneNumber = PhoneNumber.Create("0913456789").Value;
        string newFullName = $"{newFirstName.Value} {newLastName.Value}";

        contact.UpdateContact(newFirstName, newLastName, newEmail, newPhoneNumber);

        Assert.Equal(newFirstName, contact.FirstName);
        Assert.Equal(newLastName, contact.LastName);
        Assert.Equal(newEmail, contact.Email);
        Assert.Equal(newPhoneNumber, contact.PhoneNumber);
        Assert.Equal(newFullName, contact.FullName);
    }
}
