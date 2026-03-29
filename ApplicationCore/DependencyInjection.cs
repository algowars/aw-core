using System.Reflection;
using ApplicationCore.Domain.Account;
using ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddScoped<IAccountContext, AccountContext>();
        services.AddScoped<IAccountAppService, AccountAppService>();

        return services;
    }
}
