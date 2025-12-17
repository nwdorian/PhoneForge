namespace Console.Contacts.Update;

internal sealed record UpdateContactRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
