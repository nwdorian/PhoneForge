using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PhoneForge.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PhoneForgeDb");
        services.AddDbContext<PhoneForgeDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}
