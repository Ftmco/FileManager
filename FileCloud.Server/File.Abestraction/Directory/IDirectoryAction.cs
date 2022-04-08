using Microsoft.AspNetCore.Http;

namespace File.Abstraction;

public interface IDirectoryAction : IAsyncDisposable
{
    Task<UpsertDirectoryResponse> UpsertAsync(UpsertDirectory upsert, HttpContext httpContext);

    Task<UpsertDirectoryResponse> CreateAsync(UpsertDirectory create, Guid ownerId);

    Task<UpsertDirectoryResponse> UpdateAsync(UpsertDirectory update, Guid ownerId);

    Task DeleteAsync(Guid id);

    Task<bool> CreateDirectoryAsync(string path);
}
