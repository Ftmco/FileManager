namespace File.Abstraction;

public interface IFileViewModel : IAsyncDisposable
{
    Task<FileViewModel> CreateFileViewModel(DirectoryFile file);
}
