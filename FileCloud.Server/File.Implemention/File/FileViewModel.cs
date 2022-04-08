namespace File.Implemention;

public class FileViewModel : IFileViewModel
{
    readonly IDirectoryGet _directoryGet;

    public FileViewModel(IDirectoryGet directoryGet)
    {
        _directoryGet = directoryGet;
    }

    public async Task<ViewModel.FileViewModel> CreateFileViewModel(DirectoryFile file)
    {
        var dir = await _directoryGet.GetDirectoryAsync(file.DirectoryId);
        ViewModel.FileViewModel fileViewModel = new(Id: file.Id,
            Name: file.Name,
            CreateDate: file.CreateDate,
            Directory: dir?.Name ?? "",
            DireToken: dir?.Token ?? "",
            Token: file.Token);
        return fileViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
