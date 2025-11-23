using Application.Core.Abstractions.Messaging;

namespace Application.Contacts.Delete;

/// <summary>
/// Represents the command for deleting a contact.
/// </summary>
/// <param name="Id">The unique identifier of the contact to delete.</param>
public sealed record DeleteContactCommand(Guid Id) : ICommand;
