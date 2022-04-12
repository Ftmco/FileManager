using File.Extension.Code;
using File.ViewModel;
using Microsoft.Extensions.Configuration;

namespace File.Implemention;

public class FileAction : IFileAction
{
    readonly IDirectoryGet _directoryGet;

    readonly IBaseCud<DirectoryFile, FileContext> _fileCud;

    readonly IFileViewModel _fileViewModel;

    readonly IConfiguration _configuration;

    public FileAction(IDirectoryGet directoryGet, IBaseCud<DirectoryFile, FileContext> fileCud, IFileViewModel fileViewModel, IConfiguration configuration)
    {
        _directoryGet = directoryGet;
        _fileCud = fileCud;
        _fileViewModel = fileViewModel;
        _configuration = configuration;
    }

    public async Task<CreateFileResponse> CreateFileAsync(CreateFile file)
    {
        var dir = await _directoryGet.GetDirectoryAsync(file.DirectoryToken);
        if (dir != null)
        {
            DirectoryFile directoryFile = new()
            {
                CreateDate = DateTime.UtcNow,
                DirectoryId = dir.Id,
                FileName = file.Name,
                Mime = file.Mime,
                Eextension = file.Mime.Split("/")[1],
                Name = $"file_{20.CreateToken()}.{file.Mime.Split("/")[1]}",
                Token = $"file_{20.CreateToken()}",
            };
            if (await _fileCud.InsertAsync(directoryFile))
            {
                await SaveFileAsync(new(file.Base64, dir.Path, directoryFile.Name));
                return new CreateFileResponse(FileActionStatus.Success, await _fileViewModel.CreateFileViewModel(directoryFile));
            }
            return new CreateFileResponse(FileActionStatus.Exception, null);
        }
        return new CreateFileResponse(FileActionStatus.DirectoryNotFound, null);
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _directoryGet.DisposeAsync();
    }

    public async Task<bool> SaveFileAsync(SaveFile saveFile)
    {
        try
        {
            string rootPath = _configuration["BaseDirectories:BaseDir"];
            string path = rootPath + saveFile.Path + $"/{saveFile.Name}";
            byte[]? bytes = Convert.FromBase64String(saveFile.Base64);
            await System.IO.File.WriteAllBytesAsync(path, bytes);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
