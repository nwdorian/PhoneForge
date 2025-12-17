using Console.Core.Input;
using Spectre.Console;

namespace Console.Contacts.Delete;

internal sealed class DeleteContact(ContactsService contactsService)
{
    public async Task Handle(IReadOnlyCollection<Contact> contacts)
    {
        if (contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No contacts to remove![/]");
            UserInput.PromptAnyKeyToContinue();
            return;
        }

        int id = UserInput.PromptPositiveInteger(
            "Enter contact Id to remove:",
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

        if (
            await AnsiConsole.ConfirmAsync(
                $"Are you sure you want to delete this contact?"
            )
        )
        {
            await contactsService.DeleteContact(contact.Id);
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
}
