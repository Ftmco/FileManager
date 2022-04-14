using File.Extension.Code;
using File.ViewModel;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using static System.IO.File;
using static System.Convert;

namespace File.Implemention;

public class FileAction : IFileAction
{
    readonly IDirectoryGet _directoryGet;

    readonly IBaseCud<DirectoryFile, FileContext> _fileCud;

    readonly IBaseQuery<DirectoryFile, FileContext> _fileQuery;

    readonly IFileViewModel _fileViewModel;

    readonly IConfiguration _configuration;

    public FileAction(IDirectoryGet directoryGet, IBaseCud<DirectoryFile, FileContext> fileCud, IFileViewModel fileViewModel,
        IConfiguration configuration, IBaseQuery<DirectoryFile, FileContext> fileQuery)
    {
        _directoryGet = directoryGet;
        _fileCud = fileCud;
        _fileViewModel = fileViewModel;
        _configuration = configuration;
        _fileQuery = fileQuery;
    }

    public async Task<CreateFileResponse> CreateFileAsync(CreateFile file)
    {
        FDirectory? dir = await _directoryGet.GetDirectoryAsync(file.DirectoryToken);
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
            if (await SaveFileOnDiskAsync(new(file.Base64, dir.Path, directoryFile.Name)))
            {
                if (await _fileCud.InsertAsync(directoryFile))
                    return new CreateFileResponse(FileActionStatus.Success, await _fileViewModel.CreateFileViewModel(directoryFile));
                else
                {
                    await DeleteFileFromDiskAsync($"{dir}/{directoryFile.Name}");
                    return new CreateFileResponse(FileActionStatus.Exception, null);
                }
            }
            return new CreateFileResponse(FileActionStatus.Exception, null);
        }
        return new CreateFileResponse(FileActionStatus.DirectoryNotFound, null);
    }

    public async Task DeleteFileAsync(Guid id)
    {
        var file = await _fileQuery.GetAsync(id);
        if (file != null)
            await DeleteFileAsync(file);
    }

    public async Task DeleteFileAsync(string token)
    {
        var file = await _fileQuery.GetAsync(f=> f.Token == token);
        if (file != null)
            await DeleteFileAsync(file);
    }

    public async Task DeleteFileAsync(DirectoryFile file)
    {
        var dir = await _directoryGet.GetDirectoryAsync(file.DirectoryId);
        if(dir != null)
        {
            string path = $"{dir.Path}/{file.Name}";
            await DeleteFileFromDiskAsync(path);
        }
    }

    public Task<bool> DeleteFileFromDiskAsync(string path)
    {
        try
        {
            if (Exists(path))
                Delete(path);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _directoryGet.DisposeAsync();
    }

    public async Task<bool> SaveFileOnDiskAsync(SaveFile saveFile)
    {
        try
        {
            string[] base64Split = saveFile.Base64.Split(",");
            string rootPath = _configuration["BaseDirectories:BaseDir"];
            string path = rootPath + saveFile.Path + $"/{saveFile.Name}";
            byte[]? bytes = FromBase64String(base64Split.Last());
            await WriteAllBytesAsync(path, bytes);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
