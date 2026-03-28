using ApplicationCore;
using ApplicationCore.Settings;
using Asp.Versioning;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionStringsOptions = new ConnectionStringsOptions();
builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStringsOptions);

builder.Services.AddApplicationCore();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
});

string licenseKey = builder.Configuration["MediatR:LicenseKey"];

builder.Services.AddMediatR(cfg =>
{
    cfg.LicenseKey = licenseKey;
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddOpenApi();

string[] allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

if (!allowedOrigins.Any())
{
    throw new InvalidOperationException("No allowed origins configured for CORS.");
}


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
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