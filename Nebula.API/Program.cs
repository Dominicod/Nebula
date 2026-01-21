using Asp.Versioning;
using Nebula.Infrastructure.Extensions.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

// Services
builder.Services.AddOpenApi();
builder.Services.AddSqlServer(builder.Configuration);
builder.Services.AddNebulaServices();
builder.Services.AddValidation(); // Allows data-attributes to be assigned to requests for model validation
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapOpenApi();

// Using
app.UseHttpsRedirection();

app.Run();