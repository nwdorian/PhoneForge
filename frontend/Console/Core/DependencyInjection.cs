using Console.Contacts;
using Console.Contacts.Get;
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

        services.AddTransient<ContactsMenu>();
        services.AddTransient<GetContacts>();

        string apiAdress =
            configuration.GetValue<string>("ApiSettings:PhoneForgeApiAddress")
            ?? throw new InvalidOperationException("Missing connection string.");

        services
            .AddRefitClient<IContactsClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiAdress));
    }
}
