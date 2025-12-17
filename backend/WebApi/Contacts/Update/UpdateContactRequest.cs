namespace WebApi.Contacts.Update;

/// <summary>
/// The request to update a contact.
/// </summary>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
/// <param name="Email">The email.</param>
/// <param name="PhoneNumber">The phone number.</param>
public sealed record UpdateContactRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
