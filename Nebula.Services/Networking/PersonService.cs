using Nebula.Contracts.Services.Networking;
using Nebula.DataTransfer.Common;
using Nebula.DataTransfer.Contracts.Networking;

namespace Nebula.Services.Networking;

/// <inheritdoc/>
public sealed class PersonService : IPersonService
{
    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> CreateAsync(CreatePersonRequest request, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> UpdateAsync(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    /// <inheritdoc />
    public async Task<TypedResult<PersonResponse>> DeleteAsync(Guid id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}