using ClosedXML.Excel;
using Domain.Contacts;
using Domain.Core.Primitives;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Documents;

/// <summary>
/// Represents the excel service.
/// </summary>
/// <param name="logger">The logger used for diagnostic and error output.</param>
public class ExcelService(ILogger<ExcelService> logger)
{
    /// <summary>
    /// Reads contacts from the Excel document and loads them into memory.
    /// </summary>
    /// <remarks>
    /// This method processes each row of the Excel worksheet and attempts to
    /// create a <see cref="Contact"/> instance using the domain value objects.
    /// If any validation error occurs while reading a row, the operation is aborted
    /// and the error is logged.
    /// </remarks>
    /// <returns>A <see cref="Result"/> containing a list of successfully parsed <see cref="Contact"/> entities,
    /// or an error describing why loading failed.
    /// </returns>
    public async Task<Result<List<Contact>>> GetContacts()
    {
        logger.LogInformation("Loading contacts from excel document.");

        List<Contact> contacts = [];

        using XLWorkbook workbook = new("../../documents/contacts.xlsx");

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
                return firstFailOrSuccess.Error;
            }

            Contact contact = Contact.Create(
                firstName.Value,
                lastName.Value,
                email.Value,
                phoneNumber.Value
            );

            contacts.Add(contact);
        }

        if (contacts.Count == 0)
        {
            logger.LogInformation("No contacts found in excel document.");
            return Error.Failure(
                "ExcelDocument.Empty",
                "No contacts found in excel document."
            );
        }

        logger.LogInformation(
            "Successfully retrieved {Count} contacts from excel document.",
            contacts.Count
        );

        return contacts;
    }
}
