// Template: Minimal API Routes
// Location: Nebula.API/Routes/{Domain}/{EntityName}Routes.cs
// Example: Nebula.API/Routes/Networking/PersonRoutes.cs

using Microsoft.AspNetCore.Mvc;
using Nebula.Contracts.Services.{Domain};
using Nebula.DataTransfer.Contracts.{Domain};

namespace Nebula.API.Routes.{Domain};

/// <summary>
///     Defines HTTP endpoints for {EntityName} operations.
/// </summary>
public static class {EntityName}Routes
{
    /// <summary>
    ///     Maps {EntityName} routes to the endpoint route builder.
    /// </summary>
    public static void Map{EntityName}Routes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/{entityNames}")
            .WithTags("{EntityName}s");

        group.MapGet("/{id:guid}", GetById)
            .WithName("Get{EntityName}ById")
            .Produces<{EntityName}Response>()
            .Produces(404);

        group.MapGet("/", GetAll)
            .WithName("GetAll{EntityName}s")
            .Produces<{EntityName}ListResponse>();

        group.MapPost("/", Create)
            .WithName("Create{EntityName}")
            .Produces<{EntityName}Response>(201)
            .Produces(400);

        group.MapPut("/{id:guid}", Update)
            .WithName("Update{EntityName}")
            .Produces<{EntityName}Response>()
            .Produces(400)
            .Produces(404);

        group.MapDelete("/{id:guid}", Delete)
            .WithName("Delete{EntityName}")
            .Produces<{EntityName}Response>()
            .Produces(404);
    }

    /// <summary>
    ///     GET /api/{entityNames}/{id}
    /// </summary>
    private static async Task<IResult> GetById(
        [FromServices] I{EntityName}Service service,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(id, cancellationToken);

        return result.Success
            ? Results.Ok(result.Data)
            : Results.NotFound(result.ErrorMessages);
    }

    /// <summary>
    ///     GET /api/{entityNames}
    /// </summary>
    private static async Task<IResult> GetAll(
        [FromServices] I{EntityName}Service service,
        CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken);

        return result.Success
            ? Results.Ok(result.Data)
            : Results.BadRequest(result.ErrorMessages);
    }

    /// <summary>
    ///     POST /api/{entityNames}
    /// </summary>
    private static async Task<IResult> Create(
        [FromServices] I{EntityName}Service service,
        [FromBody] Create{EntityName}Command command,
        CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(command, cancellationToken);

        return result.Success
            ? Results.Created($"/api/{entityNames}/{result.Data!.Id}", result.Data)
            : Results.BadRequest(result.ErrorMessages);
    }

    /// <summary>
    ///     PUT /api/{entityNames}/{id}
    /// </summary>
    private static async Task<IResult> Update(
        [FromServices] I{EntityName}Service service,
        [FromRoute] Guid id,
        [FromBody] Update{EntityName}Command command,
        CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(id, command, cancellationToken);

        return result.Success
            ? Results.Ok(result.Data)
            : Results.BadRequest(result.ErrorMessages);
    }

    /// <summary>
    ///     DELETE /api/{entityNames}/{id}
    /// </summary>
    private static async Task<IResult> Delete(
        [FromServices] I{EntityName}Service service,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken);

        return result.Success
            ? Results.Ok(result.Data)
            : Results.NotFound(result.ErrorMessages);
    }
}

// Notes:
// - Use static class and extension method pattern
// - Group routes under /api/{entityNames}
// - Use WithTags for Swagger grouping
// - Map HTTP verbs to private static handler methods
// - Handler methods use [FromServices], [FromRoute], [FromBody] attributes
// - Return appropriate HTTP status codes based on TypedResult.Success
// - Register in Program.cs: app.Map{EntityName}Routes();
