namespace Application.Contacts.Create;

/// <summary>
/// Represents the create contact request.
/// </summary>
/// <param name="FirstName">The first name of the contact.</param>
/// <param name="LastName">The last name of the contact.</param>
/// <param name="Email">The email of the contact.</param>
/// <param name="PhoneNumber">The phone number of the contact.</param>
public sealed record class CreateContactRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
