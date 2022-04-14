namespace File.Abstraction;

public interface IFileAction : IAsyncDisposable
{
    Task<CreateFileResponse> CreateFileAsync(CreateFile file);

    Task DeleteFileAsync(Guid id);

    Task DeleteFileAsync(string token);

    Task DeleteFileAsync(DirectoryFile file);

    Task<bool> SaveFileOnDiskAsync(SaveFile saveFile);

    Task<bool> DeleteFileFromDiskAsync(string path);
}
