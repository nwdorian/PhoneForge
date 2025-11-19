using Microsoft.EntityFrameworkCore;
using PhoneForge.Domain.Contacts;

namespace PhoneForge.UseCases.Abstractions.Data;

/// <summary>
/// Represents the application database context interface.
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Gets the database set for contact entity.
    /// </summary>
    DbSet<Contact> Contacts { get; set; }

    /// <summary>
    /// Saves all of the pending changes in the unit of work.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities that have been saved.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
