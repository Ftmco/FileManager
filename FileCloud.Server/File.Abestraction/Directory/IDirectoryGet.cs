namespace File.Abstraction;

public interface IDirectoryGet : IAsyncDisposable
{
    Task<FDirectory?> GetDirectoryAsync(string token);

    Task<FDirectory?> GetDirectoryAsync(Guid id);
}
