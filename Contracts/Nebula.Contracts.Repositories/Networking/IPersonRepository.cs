using Nebula.Domain.Entities.Networking;

namespace Nebula.Contracts.Repositories.Networking;

/// <summary>
///     Repository interface for Person entity operations.
/// </summary>
public interface IPersonRepository : IRepository<Person, Guid>;