namespace File.Implemention;

public class DirectoryGet : IDirectoryGet
{
    readonly IBaseQuery<FDirectory, FileContext> _directoryQuery;

    readonly IDirectoryViewModel _directoryViewModel;

    public DirectoryGet(IBaseQuery<FDirectory, FileContext> directoryQuery, IDirectoryViewModel directoryViewModel)
    {
        _directoryQuery = directoryQuery;
        _directoryViewModel = directoryViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<FDirectory?> GetDirectoryAsync(string token)
    {
        var dir = await _directoryQuery.GetAsync(d => d.Token == token);
        return dir;
    }

    public async Task<FDirectory?> GetDirectoryAsync(Guid id)
    {
        var dir = await _directoryQuery.GetAsync(id);
        return dir;
    }
}
