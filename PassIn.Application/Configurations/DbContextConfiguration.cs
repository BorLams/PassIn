using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PassIn.Infrastructure.Contexts;

namespace PassIn.Application.Configurations;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PassInConnection");

        services.AddDbContext<PassInDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options => options.MigrationsAssembly("PassIn.Api"));
        });

        return services;
    }
}
