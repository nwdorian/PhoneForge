using Console.Core.Input;
using Spectre.Console;

namespace Console.Contacts.Update;

internal sealed class UpdateContact(ContactsService contactsService)
{
    public async Task Handle(IReadOnlyCollection<Contact> contacts)
    {
        if (contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No contacts to update![/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }

        int id = UserInput.PromptPositiveInteger(
            "Enter contact Id to edit:",
            allowZero: false
        );
        int index = UserInput.GetValidListIndex(id, contacts);

        Contact? contact = contacts.ElementAtOrDefault(index);

        if (contact is null)
        {
            AnsiConsole.MarkupLine("[red]Contact not found![/]");
            return;
        }

        RenderInformation(contact);
        AnsiConsole.MarkupLine("Enter new information. [grey]Leave empty to skip.[/]");

        string firstName = UserInput.PromptString("First name:", allowEmpty: true);
        string lastName = UserInput.PromptString("Last name:", allowEmpty: true);
        string email = UserInput.PromptString("Email:", allowEmpty: true);
        string phoneNumber = UserInput.PromptString("Phone number:", allowEmpty: true);

        UpdateContactRequest request = new(
            string.IsNullOrWhiteSpace(firstName) ? contact.FirstName : firstName,
            string.IsNullOrWhiteSpace(lastName) ? contact.LastName : lastName,
            string.IsNullOrWhiteSpace(email) ? contact.Email : email,
            string.IsNullOrWhiteSpace(phoneNumber) ? contact.PhoneNumber : phoneNumber
        );

        RenderUpdateInformation(contact, request);

        if (await AnsiConsole.ConfirmAsync("Are you sure you want to save the changes?"))
        {
            await contactsService.UpdateContact(contact.Id, request);
        }
    }

    private static void RenderInformation(Contact contact)
    {
        Table table = new() { ShowHeaders = false, ShowRowSeparators = true };

        table.AddColumn(new TableColumn("Property").Centered());
        table.AddColumn(new TableColumn("Value").Centered());

        table.AddRow("[deepskyblue1]Name[/]", $"{contact.FullName}");
        table.AddRow("[deepskyblue1]Email[/]", $"{contact.Email}");
        table.AddRow("[deepskyblue1]Phone number[/]", $"{contact.PhoneNumber}");

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    private static void RenderUpdateInformation(
        Contact contact,
        UpdateContactRequest request
    )
    {
        Table table = new() { ShowRowSeparators = true };

        table.AddColumn(new TableColumn("").Centered());
        table.AddColumn(new TableColumn("[green]New[/]").Centered());
        table.AddColumn(new TableColumn("[grey]Old[/]").Centered());

        table.AddRow(
            "[deepskyblue1]First name[/]",
            $"{request.FirstName}",
            $"[grey]{contact.FirstName}[/]"
        );
        table.AddRow(
            "[deepskyblue1]Last name[/]",
            $"{request.LastName}",
            $"[grey]{contact.LastName}[/]"
        );
        table.AddRow(
            "[deepskyblue1]Email[/]",
            $"{request.Email}",
            $"[grey]{contact.Email}[/]"
        );
        table.AddRow(
            "[deepskyblue1]Phone number[/]",
            $"{request.PhoneNumber}",
            $"[grey]{contact.PhoneNumber}[/]"
        );

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }
}
