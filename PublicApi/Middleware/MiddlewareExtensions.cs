namespace PublicApi.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseUserContextMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AccountContextMiddleware>();
    }
}
