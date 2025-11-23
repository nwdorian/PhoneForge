using Application.Core.Abstractions;
using Application.Core.Abstractions.Data;
using Domain.Contacts;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Contacts.GetById;

/// <summary>
/// Represents the <see cref="GetContactById"/> use case.
/// </summary>
public class GetContactById : IUseCase
{
    private readonly IDbContext _context;
    private readonly ILogger<GetContactById> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="GetContactById"/> class.
    /// </summary>
    /// <param name="context">The database context used to retrieve the contact.</param>
    /// <param name="logger">The logger used to record diagnostic information.</param>
    public GetContactById(IDbContext context, ILogger<GetContactById> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Handles a <see cref="GetContactByIdQuery"/>.
    /// </summary>
    /// <param name="query">The query containing the contact identifier.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>
    /// A result containing a <see cref="ContactResponse"/> if the contact is found,
    /// or an error result if the contact does not exist.
    /// </returns>
    public async Task<Result<ContactResponse>> Handle(
        GetContactByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Processing {Query}", query);

        ContactResponse? contact = await _context
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
            _logger.LogError("Error: {@Error}", ContactErrors.NotFoundById(query.Id));
            return ContactErrors.NotFoundById(query.Id);
        }

        _logger.LogInformation("Completed {@Query}", query);
        return contact;
    }
}
