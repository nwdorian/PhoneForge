using Application.Core.Abstractions;
using Application.Core.Abstractions.Data;
using Domain.Contacts;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Contacts.Delete;

/// <summary>
/// Represents the <see cref="DeleteContact"/> use case.
/// </summary>
public class DeleteContact : IUseCase
{
    private readonly IDbContext _context;
    private readonly ILogger<DeleteContact> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteContact"/> class.
    /// </summary>
    /// <param name="context">The database context used to delete the contact.</param>
    /// <param name="logger">The logger used to record diagnostic information.</param>
    public DeleteContact(IDbContext context, ILogger<DeleteContact> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Represents the <see cref="DeleteContactCommand"/> handler.
    /// </summary>
    /// <param name="command">The command containing the contact identifier.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>
    /// A successful result if the contact was deleted
    /// or an error if the contact was not found.
    /// </returns>
    public async Task<Result> Handle(
        DeleteContactCommand command,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Processing command {Command}", command);

        Contact? contact = await _context.Contacts.SingleOrDefaultAsync(
            c => c.Id == command.Id,
            cancellationToken
        );

        if (contact is null)
        {
            _logger.LogError("Error: {@Error}", ContactErrors.NotFoundById(command.Id));
            return ContactErrors.NotFoundById(command.Id);
        }

        _context.Contacts.Remove(contact);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Completed command {@Command}", command);
        return Result.Success();
    }
}
