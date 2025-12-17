using Application.Core.Abstractions.Messaging;

namespace Application.Contacts.GetById;

/// <summary>
/// Represents the query for getting a contact by identifier.
/// </summary>
/// <param name="Id">The unique identifier of the contact to retrieve.</param>
public sealed record GetContactByIdQuery(Guid Id) : IQuery<ContactResponse>;
