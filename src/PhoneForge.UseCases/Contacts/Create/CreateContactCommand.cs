namespace PhoneForge.UseCases.Contacts.Create;

/// <summary>
/// Represents a command for creating a new contact.
/// </summary>
/// <param name="FirstName">The first name of the contact.</param>
/// <param name="LastName">The last name of the contact.</param>
/// <param name="Email">The email of the contact.</param>
/// <param name="PhoneNumber">The phone number of the contact.</param>
public sealed record CreateContactCommand(string FirstName, string LastName, string Email, string PhoneNumber);
