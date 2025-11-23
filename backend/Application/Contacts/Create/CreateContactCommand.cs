using Application.Core.Abstractions.Messaging;

namespace Application.Contacts.Create;

/// <summary>
/// Represents the create contact command.
/// </summary>
/// <param name="FirstName">The first name of the contact.</param>
/// <param name="LastName">The last name of the contact.</param>
/// <param name="Email">The email of the contact.</param>
/// <param name="PhoneNumber">The phone number of the contact.</param>
public sealed record CreateContactCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
) : ICommand<ContactResponse>;
