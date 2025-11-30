using Domain.Contacts;

namespace UnitTests.Contacts;

public class ContactCreate
{
    [Fact]
    public void CreateContact_ShouldReturnContact_WithCorrectData()
    {
        FirstName firstName = FirstName.Create("John").Value;
        LastName lastName = LastName.Create("Doe").Value;
        Email email = Email.Create("jdoe@gmail.com").Value;
        PhoneNumber phoneNumber = PhoneNumber.Create("0919876543").Value;
        string fullName = $"{firstName.Value} {lastName.Value}";

        Contact contact = Contact.Create(firstName, lastName, email, phoneNumber);

        Assert.NotNull(contact);
        Assert.NotEqual(Guid.Empty, contact.Id);
        Assert.Equal(firstName.Value, contact.FirstName);
        Assert.Equal(lastName.Value, contact.LastName);
        Assert.Equal(email.Value, contact.Email);
        Assert.Equal(phoneNumber.Value, contact.PhoneNumber);
        Assert.Equal(fullName, contact.FullName);
    }
}
