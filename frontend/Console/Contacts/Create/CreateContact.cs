using Console.Core.Input;
using Spectre.Console;

namespace Console.Contacts.Create;

internal sealed class CreateContact(ContactsService contactsService)
{
    public async Task Handle()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[blue]Creating a new contact...[/]");

        string firstName = UserInput.PromptString("First name: ", allowEmpty: false);
        string lastName = UserInput.PromptString("Last name: ", allowEmpty: false);
        string email = UserInput.PromptEmail("Email: ", allowEmpty: false);
        string phoneNumber = UserInput.PromptPhoneNumber(
            "Phone number: ",
            allowEmpty: false
        );

        CreateContactRequest request = new(firstName, lastName, email, phoneNumber);

        RenderInformation(request);

        if (
            await AnsiConsole.ConfirmAsync(
                "Are you sure you want to create a new contact?"
            )
        )
        {
            await contactsService.CreateContact(request);
        }
    }

    private static void RenderInformation(CreateContactRequest request)
    {
        Table table = new()
        {
            Title = new TableTitle("[blue]New contact[/]"),
            ShowHeaders = false,
            ShowRowSeparators = true,
        };

        table.AddColumn(new TableColumn("Property").Centered());
        table.AddColumn(new TableColumn("Value").Centered());

        table.AddRow("[deepskyblue1]First name[/]", $"{request.FirstName}");
        table.AddRow("[deepskyblue1]Last name[/]", $"{request.LastName}");
        table.AddRow("[deepskyblue1]Email[/]", $"{request.Email}");
        table.AddRow("[deepskyblue1]Phone number[/]", $"{request.PhoneNumber}");

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }
}
