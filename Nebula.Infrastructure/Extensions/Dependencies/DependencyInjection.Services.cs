using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Nebula.Contracts.Services.DailyTasks;
using Nebula.Contracts.Services.Networking;
using Nebula.DataTransfer.Contracts.DailyTasks;
using Nebula.DataTransfer.Contracts.Networking;
using Nebula.Services.DailyTasks;
using Nebula.Services.Networking;
using Nebula.Services.Validators.DailyTasks;
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
        services.AddScoped<IDailyTaskService, DailyTaskService>();

        // Register validators
        services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
        services.AddScoped<IValidator<(Guid, UpdatePersonCommand)>, UpdatePersonCommandValidator>();
        services.AddScoped<IValidator<CreateDailyTaskCommand>, CreateDailyTaskCommandValidator>();
        services.AddScoped<IValidator<(Guid, UpdateDailyTaskCommand)>, UpdateDailyTaskCommandValidator>();

        // Add FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();
    }
}