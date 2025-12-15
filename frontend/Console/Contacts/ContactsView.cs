using Console.Contacts.Create;
using Console.Contacts.Delete;
using Console.Contacts.GenerateReport;
using Console.Contacts.Get;
using Console.Contacts.Update;
using Console.Core.Input;

namespace Console.Contacts;

internal sealed class ContactsView(
    ContactsService contactsService,
    CreateContact createContact,
    DeleteContact deleteContact,
    UpdateContact updateContact,
    GenerateContactsReport generateContactsReport
)
{
    public async Task Display()
    {
        bool exit = false;

        ContactsViewState state = new();

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

            ContactsLayout.Display(response, request);

            ConsoleKeyInfo key = System.Console.ReadKey(true);

            await HandleKeyInput(key, state, response);

            if (key.Key == ConsoleKey.Escape)
            {
                exit = UserInput.ConfirmExit();
            }
        }
    }

    private async Task HandleKeyInput(
        ConsoleKeyInfo key,
        ContactsViewState state,
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
            case ConsoleKey.P:
                await generateContactsReport.Handle(state, response.Items);
                break;
            case ConsoleKey.A:
                await createContact.Handle();
                break;
            case ConsoleKey.R:
                await deleteContact.Handle(response.Items);
                break;
            case ConsoleKey.E:
                await updateContact.Handle(response.Items);
                break;
        }
    }
}
