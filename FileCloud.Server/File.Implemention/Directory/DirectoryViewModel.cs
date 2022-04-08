using File.Entity;

namespace File.Implemention;

public class DirectoryViewModel : IDirectoryViewModel
{
    public Task<ViewModel.DirectoryViewModel> CreateDirectoryViewModelAsync(FDirectory directory)
    {
        ViewModel.DirectoryViewModel directoryViewModel = new(
            Id: directory.Id,
            ParentId: directory.ParentId,
            Name: directory.Name,
            Token: directory.Token,
            CreateDate: directory.CreateDate,
            LastUpdate: directory.LastUpdateDate);
        return Task.FromResult(directoryViewModel);
    }

    public async Task<IEnumerable<ViewModel.DirectoryViewModel>> CreateDirectoryViewModelAsync(IEnumerable<FDirectory> directories)
    {
        List<ViewModel.DirectoryViewModel> directoriesViewModel = new();
        foreach (var dir in directories)
            directoriesViewModel.Add(await CreateDirectoryViewModelAsync(dir));
        return directoriesViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
