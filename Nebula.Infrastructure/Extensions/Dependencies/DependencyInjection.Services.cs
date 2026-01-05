using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Nebula.Contracts.Services.Networking;
using Nebula.DataTransfer.Contracts.Networking;
using Nebula.Services.Networking;
using Nebula.Services.Validators.Networking;

namespace Nebula.Infrastructure.Extensions.Dependencies;

/// <summary>
///     Extension methods for registering service dependencies.
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    ///     Adds application services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static void AddNebulaServices(this IServiceCollection services)
    {
        // Register services
        services.AddScoped<IPersonService, PersonService>();

        // Register validators
        services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
        services.AddScoped<IValidator<(Guid, UpdatePersonCommand)>, UpdatePersonCommandValidator>();

        // Add FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();
    }
}