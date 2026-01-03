using Nebula.Architecture.Extensions.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddOpenApi();
builder.Services.AddNebulaSql(builder.Configuration);

// Configuration
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Using
app.UseHttpsRedirection();

app.Run();