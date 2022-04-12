namespace File.Implemention;

public class DirectoryViewModel : IDirectoryViewModel
{
    readonly IBaseQuery<FDirectory, FileContext> _directoryQuery;

    public DirectoryViewModel(IBaseQuery<FDirectory, FileContext> directoryQuery)
    {
        _directoryQuery = directoryQuery;
    }

    public async Task<ViewModel.DirectoryViewModel> CreateDirectoryViewModelAsync(FDirectory directory)
    {
        var childs = await _directoryQuery.GetAllAsync(d => d.ParentId == directory.Id);
        ViewModel.DirectoryViewModel directoryViewModel = new(
            Id: directory.Id,
            ParentId: directory.ParentId,
            Name: directory.Name,
            Token: directory.Token,
            CreateDate: directory.CreateDate,
            LastUpdate: directory.LastUpdateDate,
            Children: await CreateDirectoryViewModelAsync(childs));
        return directoryViewModel;
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
