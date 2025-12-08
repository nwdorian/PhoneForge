using Domain.Contacts;
using TestData.Contacts;
using UnitTests.Contacts.Cases;

namespace UnitTests.Contacts;

public class ContactTests
{
    [Theory]
    [ClassData(typeof(ContactValid))]
    public void Create_Should_ReturnContact_WithValidData(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber,
        string fullName
    )
    {
        Contact contact = Contact.Create(firstName, lastName, email, phoneNumber);

        Assert.NotNull(contact);
        Assert.NotEqual(Guid.Empty, contact.Id);
        Assert.Equal(firstName, contact.FirstName);
        Assert.Equal(lastName, contact.LastName);
        Assert.Equal(email, contact.Email);
        Assert.Equal(phoneNumber, contact.PhoneNumber);
        Assert.Equal(fullName, contact.FullName);
    }

    [Theory]
    [ClassData(typeof(ContactUpdate))]
    public void Update_Should_ChangeAllProperties(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber
    )
    {
        Contact contact = ContactData.ValidContact;

        contact.UpdateContact(firstName, lastName, email, phoneNumber);

        Assert.Equal(firstName, contact.FirstName);
        Assert.Equal(lastName, contact.LastName);
        Assert.Equal(email, contact.Email);
        Assert.Equal(phoneNumber, contact.PhoneNumber);
    }
}
