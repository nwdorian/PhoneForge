using System.Globalization;
using Console.Core.Input;
using Refit;
using Spectre.Console;

namespace Console.Contacts.Get;

internal sealed record Request(
    string SearchTerm,
    int Page,
    int PageSize,
    string SortColumn,
    string SortOrder
);

internal sealed record Response(
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage,
    IReadOnlyCollection<Contact> Items
);

internal sealed class GetContacts(IContactsClient contactsClient)
{
    public async Task Handle()
    {
        bool exit = false;
        string searchTerm = string.Empty;
        int page = 1;
        int pageSize = 10;
        string sortColumn = "first_name";
        string sortOrder = "asc";

        while (!exit)
        {
            Request request = new(searchTerm, page, pageSize, sortColumn, sortOrder);

            ApiResponse<Response> result = await contactsClient.GetContacts(request);

            if (!result.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(
                    CultureInfo.InvariantCulture,
                    $"[red]{result.Error.Message}[/]"
                );
                UserInput.PromptAnyKeyToContinue();
                break;
            }

            Response response = result.Content;

            DisplayContactsLayout(response.Items, request);

            AnsiConsole.MarkupLine(
                CultureInfo.InvariantCulture,
                $"{(response.HasPreviousPage ? "<" : "[grey]<[/]")} page {response.Page} of {response.TotalPages} {(response.HasNextPage ? ">" : "[grey]>[/]")}"
            );

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine(
                $"[grey]Use left and right arrow keys to navigate.[/]"
            );
            AnsiConsole.MarkupLine(
                $"[grey]Choose options from the legend on the right or press 'Escape' to exit.[/]"
            );
            AnsiConsole.WriteLine();

            ConsoleKeyInfo key = System.Console.ReadKey(true);

            if (key.Key == ConsoleKey.LeftArrow && response.HasPreviousPage)
            {
                page--;
            }

            if (key.Key == ConsoleKey.RightArrow && response.HasNextPage)
            {
                page++;
            }

            if (key.Key == ConsoleKey.S)
            {
                searchTerm = UserInput.PromptString("Search term:", allowEmpty: true);
            }

            if (key.Key == ConsoleKey.O)
            {
                sortOrder = SortOrderMenu.Display();
            }

            if (key.Key == ConsoleKey.C)
            {
                sortColumn = SortColumnMenu.Display();
            }

            if (key.Key == ConsoleKey.P)
            {
                pageSize = PageSizeMenu.Display();
            }

            if (key.Key == ConsoleKey.Escape)
            {
                exit = true;
            }
        }
    }

    private static void DisplayContactsLayout(
        IReadOnlyCollection<Contact> contacts,
        Request request
    )
    {
        Table table = new();

        table.Border(TableBorder.None);

        table.AddColumn(new TableColumn("[blue]Contacts[/]").Centered());
        table.AddColumn(new TableColumn("[blue]Options[/]").Centered());

        table.AddRow(DisplayContactsTable(contacts), DisplayPaginationTable(request));

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    private static Table DisplayContactsTable(IReadOnlyCollection<Contact> contacts)
    {
        Table table = new() { ShowRowSeparators = true };

        table.AddColumn(new TableColumn("[deepskyblue1]Id[/]").Centered());
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

    private static Table DisplayPaginationTable(Request request)
    {
        Table table = new() { ShowRowSeparators = true };

        table.AddColumn(new TableColumn("[deepskyblue1]Key[/]").Centered());
        table.AddColumn(new TableColumn("[deepskyblue1]Option[/]").Centered());
        table.AddColumn(new TableColumn($"[deepskyblue1]Value[/]").Centered());

        table.AddRow("[white on grey] S [/]", "Search term", $"{request.SearchTerm}");
        table.AddRow("[white on grey] P [/]", "Page Size", $"{request.PageSize}");
        table.AddRow(
            "[white on grey] C [/]",
            "Sort column",
            $"{FormatSortColumn(request.SortColumn)}"
        );
        table.AddRow(
            "[white on grey] O [/]",
            "Sort order",
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
}
