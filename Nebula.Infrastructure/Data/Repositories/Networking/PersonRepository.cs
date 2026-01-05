using Nebula.Contracts.Repositories.Networking;
using Nebula.Domain.Entities.Networking;

namespace Nebula.Infrastructure.Data.Repositories.Networking;

/// <inheritdoc cref="IPersonRepository" />
internal sealed class PersonRepository(NebulaDbContext context) : Repository<Person, Guid>(context), IPersonRepository;