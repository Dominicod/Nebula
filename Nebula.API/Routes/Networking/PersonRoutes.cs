using Nebula.Contracts.Services.Networking;
using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.Networking;

namespace Nebula.API.Routes.Networking;

internal static class PersonRoutes
{
    public static RouteGroupBuilder MapPersonRoutes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/v{version:apiVersion}/networking/persons")
            .WithTags("Persons");

        group.MapGet("/{id:guid}", GetPersonById)
            .WithName("GetPersonById")
            .WithSummary("Get a person by their unique identifier")
            .Produces<TypedResult<PersonResponse>>()
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status404NotFound)
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status500InternalServerError);

        group.MapGet("/", GetAllPersons)
            .WithName("GetAllPersons")
            .WithSummary("Get all persons")
            .Produces<TypedResult<PersonListResponse>>()
            .Produces<TypedResult<PersonListResponse>>(StatusCodes.Status500InternalServerError);

        group.MapPost("/", CreatePerson)
            .WithName("CreatePerson")
            .WithSummary("Create a new person")
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status201Created)
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status400BadRequest)
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status500InternalServerError);

        group.MapPut("/{id:guid}", UpdatePerson)
            .WithName("UpdatePerson")
            .WithSummary("Update an existing person")
            .Produces<TypedResult<PersonResponse>>()
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status404NotFound)
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status400BadRequest)
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{id:guid}", DeletePerson)
            .WithName("DeletePerson")
            .WithSummary("Delete a person")
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status204NoContent)
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status404NotFound)
            .Produces<TypedResult<PersonResponse>>(StatusCodes.Status500InternalServerError);

        return group;
    }

    private static async Task<IResult> GetPersonById(Guid id, IPersonService personService,
        CancellationToken cancellationToken)
    {
        var result = await personService.GetByIdAsync(id, cancellationToken);

        if (!result.IsSuccess) return Results.NotFound(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> GetAllPersons(IPersonService personService,
        CancellationToken cancellationToken)
    {
        var result = await personService.GetAllAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Error retrieving persons",
                detail: string.Join(", ", result.ErrorMessages));
        }

        return Results.Ok(result);
    }

    private static async Task<IResult> CreatePerson(CreatePersonCommand command, IPersonService personService,
        CancellationToken cancellationToken)
    {
        var result = await personService.CreateAsync(command, cancellationToken);

        if (!result.IsSuccess) return Results.BadRequest(result);

        return Results.Created($"/api/v1/networking/persons/{result.Data?.Id}", result);
    }

    private static async Task<IResult> UpdatePerson(Guid id, UpdatePersonCommand command, IPersonService personService,
        CancellationToken cancellationToken)
    {
        var result = await personService.UpdateAsync(id, command, cancellationToken);

        if (!result.IsSuccess)
        {
            if (result.ErrorMessages.Any(msg => msg.Contains("not found"))) return Results.NotFound(result);

            return Results.BadRequest(result);
        }

        return Results.Ok(result);
    }

    private static async Task<IResult> DeletePerson(Guid id, IPersonService personService,
        CancellationToken cancellationToken)
    {
        var result = await personService.DeleteAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            if (result.ErrorMessages.Any(msg => msg.Contains("not found"))) return Results.NotFound(result);

            return Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Error deleting person",
                detail: string.Join(", ", result.ErrorMessages));
        }

        return Results.NoContent();
    }
}