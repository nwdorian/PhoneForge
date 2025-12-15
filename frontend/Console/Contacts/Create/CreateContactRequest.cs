namespace Console.Contacts.Create;

internal sealed record CreateContactRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
