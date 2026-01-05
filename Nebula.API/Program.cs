using Asp.Versioning;
using Nebula.API.Routes.Networking;
using Nebula.Infrastructure.Extensions.Dependencies;

var builder = WebApplication.CreateBuilder(args);

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

// Configuration
var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapOpenApi();

// Using
app.UseHttpsRedirection();

// Routes
var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .ReportApiVersions()
    .Build();
app.MapPersonRoutes()
    .WithApiVersionSet(apiVersionSet);

app.Run();