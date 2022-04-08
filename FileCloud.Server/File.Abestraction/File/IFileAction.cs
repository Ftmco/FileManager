namespace File.Abstraction;

public interface IFileAction : IAsyncDisposable
{
    Task<CreateFileResponse> CreateFileAsync(CreateFile file);

    Task<bool> SaveFileAsync(SaveFile saveFile);
}
