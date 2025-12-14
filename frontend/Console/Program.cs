using Console.Contacts;
using Console.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services.AddConsoleServices(builder.Configuration);

using IHost host = builder.Build();

ContactsMenu mainMenu = host.Services.GetRequiredService<ContactsMenu>();

await mainMenu.Display();
