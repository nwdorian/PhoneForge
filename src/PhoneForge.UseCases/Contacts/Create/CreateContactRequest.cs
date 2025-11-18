namespace PhoneForge.UseCases.Contacts.Create;

public sealed record class CreateContactRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
