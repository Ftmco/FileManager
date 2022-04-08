namespace File.Abstraction;

public interface IDirectoryViewModel : IAsyncDisposable
{
    Task<DirectoryViewModel> CreateDirectoryViewModelAsync(FDirectory directory);

    Task<IEnumerable<DirectoryViewModel>> CreateDirectoryViewModelAsync(IEnumerable<FDirectory> directories);
}
