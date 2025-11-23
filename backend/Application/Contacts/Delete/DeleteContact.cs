using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Domain.Contacts;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Application.Contacts.Delete;

internal sealed class DeleteContact(IDbContext context)
    : ICommandHandler<DeleteContactCommand>
{
    public async Task<Result> Handle(
        DeleteContactCommand command,
        CancellationToken cancellationToken
    )
    {
        Contact? contact = await context.Contacts.SingleOrDefaultAsync(
            c => c.Id == command.Id,
            cancellationToken
        );

        if (contact is null)
        {
            return ContactErrors.NotFoundById(command.Id);
        }

        context.Contacts.Remove(contact);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
