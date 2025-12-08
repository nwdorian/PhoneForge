using TestData.PhoneNumbers;
using WebApi.Contacts.Create;

namespace TestData.Contacts.Create;

public static class CreateContactRequestData
{
    public static CreateContactRequest CreateValidRequest()
    {
        return new CreateContactRequest(
            FirstNameData.ValidFirstName,
            LastNameData.ValidLastName,
            EmailData.ValidEmail,
            PhoneNumberData.ValidPhoneNumber
        );
    }

    public static CreateContactRequest CreateRequestWithInvalidFirstName()
    {
        return new CreateContactRequest(
            FirstNameData.LongerThanAllowedFirstName,
            LastNameData.ValidLastName,
            EmailData.ValidEmail,
            PhoneNumberData.ValidPhoneNumber
        );
    }

    public static CreateContactRequest CreateRequestWithInvalidLastName()
    {
        return new CreateContactRequest(
            FirstNameData.ValidFirstName,
            LastNameData.LongerThanAllowedLastName,
            EmailData.ValidEmail,
            PhoneNumberData.ValidPhoneNumber
        );
    }

    public static CreateContactRequest CreateRequestWithInvalidEmail()
    {
        return new CreateContactRequest(
            FirstNameData.ValidFirstName,
            LastNameData.ValidLastName,
            EmailData.LongerThanAllowedEmail,
            PhoneNumberData.ValidPhoneNumber
        );
    }

    public static CreateContactRequest CreateRequestWithInvalidPhoneNumber()
    {
        return new CreateContactRequest(
            FirstNameData.ValidFirstName,
            LastNameData.ValidLastName,
            EmailData.ValidEmail,
            PhoneNumberData.LongerThanAllowedPhoneNumber
        );
    }
}
