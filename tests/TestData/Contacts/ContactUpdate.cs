using Domain.Contacts;
using Xunit;

namespace TestData.Contacts;

public class ContactUpdate : TheoryData<FirstName, LastName, Email, PhoneNumber>
{
    public ContactUpdate()
    {
        Add(
            FirstName.Create("Ada").Value,
            LastName.Create("Lovelace").Value,
            Email.Create("alovelace@gmail.com").Value,
            PhoneNumber.Create("0913456789").Value
        );
    }
}
