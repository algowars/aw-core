using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace PublicApi;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthTokenValidation(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                options.Audience = configuration["Auth:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                };
            });

        return services;
    }
}
