using System.Globalization;
using Console.Contacts.Abstractions;
using Console.Contacts.Get;
using Console.Core.Input;
using Refit;
using Spectre.Console;

namespace Console.Contacts;

internal sealed class ContactsService(IContactsClient contactsClient) : IContactsService
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
}
