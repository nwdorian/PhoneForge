using PhoneForge.WebApi.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.AddWebApplicationServices(builder.Configuration);

var app = builder.Build();

await app.UseWebApplicationMiddleware();

await app.RunAsync();
