using ApplicationCore;
using ApplicationCore.Settings;
using Asp.Versioning;
using Infrastructure;
using Microsoft.Extensions.Options;
using PublicApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterAppSettings(builder.Configuration);

builder.Services.AddApplicationCore();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();

builder.Services.RegisterAllUserAndGlobalRateLimitPolicies(typeof(Program).Assembly);

builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
});

builder.Services.AddMediatR(cfg =>
{
    cfg.LicenseKey = builder.Configuration.GetSection("MediatRSettings:LicenseKey").Get<string>();
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddOpenApi();

string[] allowedOrigins =
    builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
