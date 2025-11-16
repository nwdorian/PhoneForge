using PhoneForge.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApplicationServices(builder.Configuration);

var app = builder.Build();

await app.UseWebApplicationMiddleware();

await app.RunAsync();
