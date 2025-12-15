using Console.Contacts;
using Console.Contacts.Create;
using Console.Contacts.Delete;
using Console.Contacts.GenerateReport;
using Console.Contacts.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;

namespace Console.Core;

internal static class DependencyInjection
{
    public static void AddConsoleServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddLogging(b => b.ClearProviders());

        services.AddTransient<ContactsView>();
        services.AddTransient<CreateContact>();
        services.AddTransient<DeleteContact>();
        services.AddTransient<UpdateContact>();
        services.AddTransient<GenerateContactsReport>();
        services.AddTransient<ContactsService>();

        string apiAdress =
            configuration.GetValue<string>("ApiSettings:PhoneForgeApiAddress")
            ?? throw new InvalidOperationException("Missing connection string.");

        services
            .AddRefitClient<IContactsClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiAdress));
    }
}
