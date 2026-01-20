using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Nebula.Contracts.Services.ActionItems;
using Nebula.Contracts.Services.Networking;
using Nebula.DataTransfer.Contracts.ActionItems;
using Nebula.DataTransfer.Contracts.Networking;
using Nebula.Services.ActionItems;
using Nebula.Services.Common.Validators.ActionItems;
using Nebula.Services.Common.Validators.Networking;
using Nebula.Services.Networking;

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
        services.AddScoped<IActionItemService, ActionItemService>();

        // Register validators
        services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
        services.AddScoped<IValidator<CreateActionItemCommand>, CreateActionItemCommandValidator>();

        // Add FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();
    }
}