using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Contacts;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Application.Contacts.GetById;

internal sealed class GetContactByIdHandler(IDbContext context)
    : IQueryHandler<GetContactByIdQuery, ContactResponse>
{
    public async Task<Result<ContactResponse>> Handle(
        GetContactByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        ContactResponse? contact = await context
            .Contacts.AsNoTracking()
            .Where(c => c.Id == query.Id)
            .Select(c => new ContactResponse(
                c.Id,
                c.FirstName,
                c.LastName,
                c.FullName,
                c.Email,
                c.PhoneNumber,
                c.CreatedOnUtc
            ))
            .SingleOrDefaultAsync(cancellationToken);

        if (contact is null)
        {
            return ContactErrors.NotFoundById(query.Id);
        }

        return contact;
    }
}
