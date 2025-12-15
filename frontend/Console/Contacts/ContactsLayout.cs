using System.Globalization;
using Console.Contacts.Get;
using Spectre.Console;

namespace Console.Contacts;

internal static class ContactsLayout
{
    public static void Display(GetContactsResponse response, GetContactsRequest request)
    {
        Table table = new();

        table.Border(TableBorder.None);

        table.AddColumn(new TableColumn("[blue]Contacts[/]").Centered());
        table.AddColumn(new TableColumn("[blue]Options[/]").Centered());

        table.AddRow(RenderContacts(response.Items), RenderOptions(request));

        AnsiConsole.Clear();
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine(
            CultureInfo.InvariantCulture,
            $"{(response.HasPreviousPage ? "<" : "[grey]<[/]")} page {response.Page} of {response.TotalPages} {(response.HasNextPage ? ">" : "[grey]>[/]")}"
        );

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[grey]Use left and right arrow keys to navigate.[/]");
        AnsiConsole.MarkupLine(
            $"[grey]Press [white on grey] Escape [/] to exit or choose an option.[/]"
        );
        AnsiConsole.WriteLine();
    }

    private static Table RenderContacts(IReadOnlyCollection<Contact> contacts)
    {
        Table table = new() { ShowRowSeparators = true };

        table.AddColumn(new TableColumn("[deepskyblue1]#[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Name[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Email[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Phone number[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Created[/]").Centered());

        foreach ((int index, Contact contact) in contacts.Index())
        {
            table.AddRow(
                (index + 1).ToString(CultureInfo.InvariantCulture),
                contact.FullName,
                contact.Email,
                contact.PhoneNumber,
                contact.CreatedOnUtc.ToString(
                    "d-MMM-yyyy HH:mm",
                    CultureInfo.InvariantCulture
                )
            );
        }
        return table;
    }

    private static Table RenderOptions(GetContactsRequest request)
    {
        Table table = new() { ShowHeaders = false };

        table.Border(TableBorder.None);

        table.AddColumn(new TableColumn("").Centered());

        table.AddRow(RenderPagination(request));
        table.AddRow(RenderFeatures());

        return table;
    }

    private static Table RenderPagination(GetContactsRequest request)
    {
        Table table = new() { ShowRowSeparators = true };

        table.AddColumn(new TableColumn("[deepskyblue1]Key[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Option[/]").Centered());
        table.AddColumn(new TableColumn($"[deepskyblue1]Value[/]").Centered());

        table.AddRow("[white on grey] F [/]", "Search term", $"{request.SearchTerm}");
        table.AddRow("[white on grey] S [/]", "Page size", $"{request.PageSize}");
        table.AddRow(
            "[white on grey] C [/]",
            "Sort column",
            $"{FormatSortColumn(request.SortColumn)}"
        );
        table.AddRow(
            "[white on grey] D [/]",
            "Sort direction",
            $"{FormatSortOrder(request.SortOrder)}"
        );

        return table;

        static string FormatSortColumn(string sortColumn)
        {
            return sortColumn switch
            {
                "first_name" => "First name",
                "last_name" => "Last name",
                "email" => "Email",
                "phone_number" => "Phone number",
                "created_on" => "Created",
                _ => string.Empty,
            };
        }

        static string FormatSortOrder(string sortOrder)
        {
            return sortOrder switch
            {
                "asc" => "Ascending",
                "desc" => "Descending",
                _ => string.Empty,
            };
        }
    }

    private static Table RenderFeatures()
    {
        Table table = new() { ShowRowSeparators = true, ShowHeaders = false };

        table.AddColumn(new TableColumn("").Centered());
        table.AddColumn(new TableColumn("").Centered());

        table.AddRow("[white on grey] A [/]", "Add contact");
        table.AddRow("[white on grey] R [/]", "Remove contact");
        table.AddRow("[white on grey] E [/]", "Edit contact");
        table.AddRow("[white on grey] P [/]", "Create report");

        return table;
    }
}
