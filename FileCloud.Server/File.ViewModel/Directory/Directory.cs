namespace File.ViewModel;

public record DirectoryViewModel(Guid Id, Guid? ParentId, string Name, string Token, DateTime CreateDate, DateTime LastUpdate, IEnumerable<DirectoryViewModel> Children);

public record UpsertDirectory(Guid? ParentId, Guid? Id, string Name);

public record UpsertDirectoryResponse(DirecttoryActionStatus Status, DirectoryViewModel? Directory);

public enum DirecttoryActionStatus
{
    Success = 0,
    AccessDenied = 1,
    Exception = 2,
    ObjectNotFound = 3
}
