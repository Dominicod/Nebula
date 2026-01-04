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

    private static async Task<IResult> CreatePerson(CreatePersonCommand command, IPersonService personService,
        CancellationToken cancellationToken) => throw new NotImplementedException();

    private static async Task<IResult> UpdatePerson(Guid id, UpdatePersonCommand command, IPersonService personService,
        CancellationToken cancellationToken) => throw new NotImplementedException();

    private static async Task<IResult> DeletePerson(Guid id, IPersonService personService,
        CancellationToken cancellationToken) => throw new NotImplementedException();
}