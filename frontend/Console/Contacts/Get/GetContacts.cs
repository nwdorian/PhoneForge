using Console.Contacts.Abstractions;
using Console.Core.Input;
using Spectre.Console;

namespace Console.Contacts.Get;

internal sealed class GetContacts(IContactsService contactsService)
{
    public async Task Handle()
    {
        bool exit = false;

        GetContactsState state = new();

        while (!exit)
        {
            GetContactsRequest request = new(
                state.SearchTerm,
                state.Page,
                state.PageSize,
                state.SortColumn,
                state.SortOrder
            );

            GetContactsResponse? response = await contactsService.GetContacts(request);

            if (response is null)
            {
                break;
            }

            ContactsTables.RenderLayout(response, request);

            ConsoleKeyInfo key = System.Console.ReadKey(true);

            await HandleKeyInput(key, state, response);

            if (key.Key == ConsoleKey.Escape)
            {
                exit = true;
            }
        }
    }

    private async Task HandleKeyInput(
        ConsoleKeyInfo key,
        GetContactsState state,
        GetContactsResponse response
    )
    {
        switch (key.Key)
        {
            case ConsoleKey.LeftArrow when response.HasPreviousPage:
                state.Page--;
                break;
            case ConsoleKey.RightArrow when response.HasNextPage:
                state.Page++;
                break;
            case ConsoleKey.F:
                state.SearchTerm = UserInput.PromptString(
                    "Search term:",
                    allowEmpty: true
                );
                state.Page = 1;
                break;
            case ConsoleKey.D:
                state.SortOrder = UserInput.PromptSortOrder();
                state.Page = 1;
                break;
            case ConsoleKey.C:
                state.SortColumn = UserInput.PromptContactsSortColumn();
                state.Page = 1;
                break;
            case ConsoleKey.S:
                state.PageSize = UserInput.PromptPageSize();
                state.Page = 1;
                break;
            case ConsoleKey.R:
                await GenerateReport(state);
                break;
        }
    }

    private async Task GenerateReport(GetContactsState state)
    {
        if (
            !await AnsiConsole.ConfirmAsync(
                "Are you sure you want to generate a PDF report?"
            )
        )
        {
            return;
        }

        GenerateContactsReportRequest contactsReportRequest = new(
            state.SearchTerm,
            state.SortColumn,
            state.SortOrder
        );

        await contactsService.GenerateReport(contactsReportRequest);
    }
}
