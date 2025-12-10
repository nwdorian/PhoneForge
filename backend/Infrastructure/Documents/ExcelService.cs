using ClosedXML.Excel;
using Domain.Contacts;
using Domain.Core.Primitives;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Documents;

/// <summary>
/// Provides functionality for interacting with excel documents.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="logger">The logger used for diagnostic and error output</param>
public class ExcelService(PhoneForgeDbContext context, ILogger<ExcelService> logger)
{
    /// <summary>
    /// Seeds data from excel spreadsheet to the database.
    /// </summary>
    /// <returns>The result of seeding process or an error.</returns>
    public async Task SeedExcelDataAsync()
    {
        logger.LogInformation("Started seeding the database from excel document.");

        if (await context.Contacts.AnyAsync())
        {
            logger.LogWarning("Data already exists in the database. Aborting process.");
            return;
        }

        List<Contact> contacts = [];

        using XLWorkbook workbook = new("documents/contacts.xlsx");

        IXLWorksheet worksheet = workbook.Worksheet(1);

        foreach (IXLRow? row in worksheet.RowsUsed())
        {
            Result<FirstName> firstName = FirstName.Create(row.Cell(1).GetString());
            Result<LastName> lastName = LastName.Create(row.Cell(2).GetString());
            Result<Email> email = Email.Create(row.Cell(3).GetString());
            Result<PhoneNumber> phoneNumber = PhoneNumber.Create(row.Cell(4).GetString());

            Result firstFailOrSuccess = Result.FirstFailOrSuccess(
                firstName,
                lastName,
                email,
                phoneNumber
            );

            if (firstFailOrSuccess.IsFailure)
            {
                logger.LogError(
                    "There was an error loading excel data at row {Row}. Error: {@Error}",
                    row.RowNumber(),
                    firstFailOrSuccess.Error
                );
                return;
            }

            Contact contact = Contact.Create(
                firstName.Value,
                lastName.Value,
                email.Value,
                phoneNumber.Value
            );

            contacts.Add(contact);
        }

        context.AddRange(contacts);

        await context.SaveChangesAsync();

        logger.LogInformation("Successfully seeded the database from excel document.");
    }
}
