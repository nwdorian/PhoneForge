using Domain.Contacts;
using Domain.Core.Primitives;
using Infrastructure.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database.Seeding;

/// <summary>
/// Represents the seeding service.
/// </summary>
/// <param name="excelService">The excel service.</param>
/// <param name="context">The EF Core database context.</param>
/// <param name="logger">The logger used for diagnostic and error output.</param>
public class SeedingService(
    ExcelService excelService,
    PhoneForgeDbContext context,
    ILogger<SeedingService> logger
)
{
    /// <summary>
    /// Reads contacts from the Excel document and seeds them into the database.
    /// </summary>
    /// <remarks>
    /// This method attempts to load contacts using <see cref="ExcelService.GetContacts"/>.
    /// If loading fails for any reason (e.g., invalid data),
    /// the method exits without modifying the database.
    /// Seeding is skipped when the contacts table already contains data.
    /// </remarks>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The task completes when the contacts have been saved,
    /// or exits early if excel parsing failed or the database already contains contacts.
    /// </returns>
    public async Task SeedContacts()
    {
        logger.LogInformation("Seeding contacts to the database.");

        if (await context.Contacts.AnyAsync())
        {
            logger.LogWarning(
                "Contacts already exists in the database. Aborting process."
            );
            return;
        }

        Result<List<Contact>> getContactsResult = await excelService.GetContacts();

        if (getContactsResult.IsFailure)
        {
            return;
        }

        List<Contact> contacts = getContactsResult.Value;

        context.AddRange(contacts);

        await context.SaveChangesAsync();
        logger.LogInformation(
            "Successfully seeded {Count} contacts to the database.",
            contacts.Count
        );
    }
}
