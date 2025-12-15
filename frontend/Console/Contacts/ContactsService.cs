using System.Globalization;
using Console.Contacts.Create;
using Console.Contacts.GenerateReport;
using Console.Contacts.Get;
using Console.Contacts.Update;
using Console.Core.Input;
using Refit;
using Spectre.Console;

namespace Console.Contacts;

internal sealed class ContactsService(IContactsClient contactsClient)
{
    public async Task<GetContactsResponse?> GetContacts(GetContactsRequest request)
    {
        try
        {
            ApiResponse<GetContactsResponse> result = await contactsClient.GetContacts(
                request
            );

            if (!result.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(
                    CultureInfo.InvariantCulture,
                    $"[red]{result.Error.Message}[/]"
                );
                UserInput.PromptAnyKeyToContinue();
            }

            return result.Content;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();

            return null;
        }
    }

    public async Task GenerateReport(GenerateContactsReportRequest request)
    {
        try
        {
            IApiResponse result = await contactsClient.GenerateContactsReport(request);

            if (!result.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(
                    CultureInfo.InvariantCulture,
                    $"[red]{result.Error.Message}[/]"
                );
                UserInput.PromptAnyKeyToContinue();
                return;
            }

            AnsiConsole.MarkupLine("[green]Report generated successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }

    public async Task CreateContact(CreateContactRequest request)
    {
        try
        {
            IApiResponse result = await contactsClient.CreateContact(request);

            if (!result.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(
                    CultureInfo.InvariantCulture,
                    $"[red]{result.Error.Message}[/]"
                );
                UserInput.PromptAnyKeyToContinue();
                return;
            }

            AnsiConsole.MarkupLine("[green]Contact created successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }

    public async Task DeleteContact(Guid id)
    {
        try
        {
            IApiResponse result = await contactsClient.DeleteContact(id);

            if (!result.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(
                    CultureInfo.InvariantCulture,
                    $"[red]{result.Error.Message}[/]"
                );
                UserInput.PromptAnyKeyToContinue();
                return;
            }

            AnsiConsole.MarkupLine("[green]Contact deleted successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }

    public async Task UpdateContact(Guid id, UpdateContactRequest request)
    {
        try
        {
            IApiResponse result = await contactsClient.UpdateContact(id, request);

            if (!result.IsSuccessful)
            {
                AnsiConsole.MarkupLineInterpolated(
                    CultureInfo.InvariantCulture,
                    $"[red]{result.Error.Message}[/]"
                );
                UserInput.PromptAnyKeyToContinue();
                return;
            }

            AnsiConsole.MarkupLine("[green]Contact updated successfully![/]");
            UserInput.PromptAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]There was an error: {ex.Message}[/]");
            UserInput.PromptAnyKeyToContinue();
        }
    }
}
