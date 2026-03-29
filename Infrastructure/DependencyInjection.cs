using ApplicationCore.Settings;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(
            (sp, options) =>
            {
                var connectionStringOptions = sp.GetRequiredService<
                    IOptions<ConnectionStringsSettings>
                >().Value;
                options.UseNpgsql(connectionStringOptions.DefaultConnection);
            }
        );

        return services;
    }
}