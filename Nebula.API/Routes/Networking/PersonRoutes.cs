using Nebula.Services.Contracts.Networking;
using Nebula.Services.Networking;

namespace Nebula.API.Routes.Networking;

public static class PersonRoutes
{
    public static RouteGroupBuilder MapPersonRoutes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/v{version:apiVersion}/persons")
            .WithTags("Persons");

        group.MapGet("/", GetAllPersons)
            .WithName("GetAllPersons")
            .WithSummary("Get all persons")
            .Produces<IEnumerable<PersonResponse>>();

        group.MapGet("/{id:guid}", GetPersonById)
            .WithName("GetPersonById")
            .WithSummary("Get a person by ID")
            .Produces<PersonResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", CreatePerson)
            .WithName("CreatePerson")
            .WithSummary("Create a new person")
            .Produces<PersonResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:guid}", UpdatePerson)
            .WithName("UpdatePerson")
            .WithSummary("Update an existing person")
            .Produces<PersonResponse>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapDelete("/{id:guid}", DeletePerson)
            .WithName("DeletePerson")
            .WithSummary("Delete a person")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        return group;
    }

    private static async Task<IResult> GetAllPersons(IPersonService personService, CancellationToken cancellationToken)
    {
        var persons = await personService.GetAllAsync(cancellationToken);
        return Results.Ok(persons);
    }

    private static async Task<IResult> GetPersonById(Guid id, IPersonService personService, CancellationToken cancellationToken)
    {
        var person = await personService.GetByIdAsync(id, cancellationToken);

        if (person == null)
            return Results.NotFound();

        return Results.Ok(person);
    }

    private static async Task<IResult> CreatePerson(CreatePersonRequest request, IPersonService personService, CancellationToken cancellationToken)
    {
        var person = await personService.CreateAsync(request, cancellationToken);
        return Results.Created($"/api/v1/persons/{person.Id}", person);
    }

    private static async Task<IResult> UpdatePerson(Guid id, UpdatePersonRequest request, IPersonService personService, CancellationToken cancellationToken)
    {
        var person = await personService.UpdateAsync(id, request, cancellationToken);

        if (person == null)
            return Results.NotFound();

        return Results.Ok(person);
    }

    private static async Task<IResult> DeletePerson(Guid id, IPersonService personService, CancellationToken cancellationToken)
    {
        var deleted = await personService.DeleteAsync(id, cancellationToken);

        if (!deleted)
            return Results.NotFound();

        return Results.NoContent();
    }
}