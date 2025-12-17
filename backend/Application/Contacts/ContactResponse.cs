namespace Application.Contacts;

/// <summary>
/// Represents the response returned when retrieving or creating a contact.
/// </summary>
/// <param name="Id">The unique identifier of the contact.</param>
/// <param name="FirstName">The first name of the contact.</param>
/// <param name="LastName">The last name of the contact.</param>
/// <param name="FullName">The full name of the contact.</param>
/// <param name="Email">The email address of the contact.</param>
/// <param name="PhoneNumber">The phone number of the contact.</param>
/// <param name="CreatedOnUtc">The date and time when the contact was created in UTC format.</param>
public record ContactResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime CreatedOnUtc
);
