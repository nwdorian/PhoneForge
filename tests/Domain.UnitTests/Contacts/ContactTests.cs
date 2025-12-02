using Domain.Contacts;

namespace Domain.UnitTests.Contacts;

public class ContactTests
{
    private static readonly FirstName _firstName = FirstName.Create("John").Value;
    private static readonly LastName _lastName = LastName.Create("Doe").Value;
    private static readonly Email _email = Email.Create("jdoe@gmail.com").Value;
    private static readonly PhoneNumber _phoneNumber = PhoneNumber
        .Create("0919876543")
        .Value;
    private static string _fullName => $"{_firstName.Value} {_lastName.Value}";

    [Fact]
    public void Create_Should_ReturnContact_WithValidData()
    {
        Contact contact = Contact.Create(_firstName, _lastName, _email, _phoneNumber);

        Assert.NotNull(contact);
        Assert.NotEqual(Guid.Empty, contact.Id);
        Assert.Equal(_firstName, contact.FirstName);
        Assert.Equal(_lastName, contact.LastName);
        Assert.Equal(_email, contact.Email);
        Assert.Equal(_phoneNumber, contact.PhoneNumber);
        Assert.Equal(_fullName, contact.FullName);
    }

    [Fact]
    public void Update_Should_ChangeAllProperties()
    {
        Contact contact = Contact.Create(_firstName, _lastName, _email, _phoneNumber);

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
