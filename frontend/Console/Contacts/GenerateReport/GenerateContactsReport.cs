using Console.Core.Input;
using Spectre.Console;

namespace Console.Contacts.GenerateReport;

internal sealed class GenerateContactsReport(ContactsService contactsService)
{
    public async Task Handle(
        ContactsViewState state,
        IReadOnlyCollection<Contact> contacts
    )
    {
        if (contacts.Count == 0)
        {
            AnsiConsole.MarkupLine(
                "[red]Unable to generate a report with no contacts![/]"
            );
            AnsiConsole.MarkupLine("[grey]Edit filters or create contacts.[/]");
            AnsiConsole.WriteLine();
            UserInput.PromptAnyKeyToContinue();
            return;
        }

        GenerateContactsReportRequest contactsReportRequest = new(
            state.SearchTerm,
            state.SortColumn,
            state.SortOrder
        );

        AnsiConsole.MarkupLine("Are you sure you want to generate a PDF report?");
        if (await AnsiConsole.ConfirmAsync("Current filters will apply."))
        {
            await contactsService.GenerateReport(contactsReportRequest);
        }
    }
}
