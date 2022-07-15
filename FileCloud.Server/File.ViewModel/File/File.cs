namespace File.ViewModel;

public record FileViewModel(Guid Id, string Name, DateTime CreateDate, string Directory, string DireToken, string Token);

public record CreateFile(string DirectoryToken, string Base64, string Name, string Mime);

public record CreateFileResponse(FileActionStatus Status, FileViewModel? File);

public enum FileActionStatus
{
    Success = 0,
    AccessDenied = 1,
    Exception = 2,
    NotFound = 3,
    DirectoryNotFound = 4,
}

public record SaveFile(string Base64, string Path, string Name);

public record GetFileResponse(FileActionStatus Status,string Mime,string Extension)
{
    public byte[]? ContentBytes { get; set; }

    public string? Base64 { get; set; }
}