using System.Globalization;
using System.Linq.Expressions;
using Application.Core.Abstractions.Data;
using Application.Core.Abstractions.Messaging;
using Application.Core.Abstractions.Reports;
using Domain.Contacts;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Application.Contacts.GenerateReport;

internal class GenerateContactsReportHandler(
    IDbContext context,
    IReportsService reportsService
) : ICommandHandler<GenerateContactsReportCommand>
{
    public async Task<Result> Handle(
        GenerateContactsReportCommand command,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Contact> contactsQuery = context.Contacts;

        if (!string.IsNullOrWhiteSpace(command.SearchTerm))
        {
            contactsQuery = contactsQuery.Where(c =>
                c.FirstName.Value.Contains(command.SearchTerm)
                || c.LastName.Value.Contains(command.SearchTerm)
                || c.Email.Value.Contains(command.SearchTerm)
                || c.PhoneNumber.Value.Contains(command.SearchTerm)
            );
        }

        if (command.SortOrder?.ToLower(CultureInfo.InvariantCulture) == "desc")
        {
            contactsQuery = contactsQuery.OrderByDescending(GetSortProperty(command));
        }
        else
        {
            contactsQuery = contactsQuery.OrderBy(GetSortProperty(command));
        }

        List<ContactResponse> contacts = await contactsQuery
            .Select(c => new ContactResponse(
                c.Id,
                c.FirstName,
                c.LastName,
                c.FullName,
                c.Email,
                c.PhoneNumber,
                c.CreatedOnUtc
            ))
            .ToListAsync(cancellationToken);

        await reportsService.GenerateContactsReport(contacts);

        return Result.Success();
    }

    private static Expression<Func<Contact, object>> GetSortProperty(
        GenerateContactsReportCommand command
    )
    {
        return command.SortColumn.ToLower(CultureInfo.InvariantCulture) switch
        {
            "first_name" => contact => contact.FirstName.Value,
            "last_name" => contact => contact.LastName.Value,
            "email" => contact => contact.Email.Value,
            "phone_number" => contact => contact.PhoneNumber.Value,
            "created_on" => contact => contact.CreatedOnUtc,
            _ => contact => contact.FirstName.Value,
        };
    }
}
