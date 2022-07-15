using File.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Implemention;

public class FileGet : IFileGet
{
    readonly IBaseQuery<DirectoryFile, FileContext> _fileQuery;

    readonly IDirectoryGet _directoryGet;

    readonly IConfiguration _configuration;

    public FileGet(IBaseQuery<DirectoryFile, FileContext> fileQuery, IDirectoryGet directoryGet, IConfiguration configuration)
    {
        _fileQuery = fileQuery;
        _directoryGet = directoryGet;
        _configuration = configuration;
    }

    public Task<string> CreateDataUrlAsync(string base64, string mime)
    {
        string dataUrl = $"data:{mime};base64,{base64}";
        return Task.FromResult(dataUrl);
    }

    public Task<GetFileResponse> CropImageAsync(string token, int width, int heigth)
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<GetFileResponse> GetFileBase64Async(string token)
    {
        try
        {
            DirectoryFile? file = await _fileQuery.GetAsync(f => f.Token == token);
            if (file != null)
            {
                FDirectory? dir = await _directoryGet.GetDirectoryAsync(file.DirectoryId);
                if (dir != null)
                {
                    var rootDir = _configuration["BaseDirectories:BaseDir"];
                    string? path = $"{rootDir}{dir.Path}/{file.Name}";
                    byte[]? bytes = await System.IO.File.ReadAllBytesAsync(path);
                    string? base64 = Convert.ToBase64String(bytes);
                    return new GetFileResponse(FileActionStatus.Success, file.Mime, file.Eextension)
                    {
                        Base64 = await CreateDataUrlAsync(base64, file.Mime)
                    };
                }
                return new GetFileResponse(FileActionStatus.DirectoryNotFound, "", "");
            }
            return new GetFileResponse(FileActionStatus.NotFound, "", "");
        }
        catch
        {
            return new GetFileResponse(FileActionStatus.Exception, "", "");
        }
    }

    public async Task<GetFileResponse?> GetImageAsync(string token, int width, int heigth, string action)
        => action switch
        {
            "resize" => await ResizeImageAsync(token, width, heigth),
            "crop" => await CropImageAsync(token, width, heigth),
            _ => await ResizeImageAsync(token, width, heigth),
        };

    public Task<GetFileResponse> ResizeImageAsync(string token, int width, int heigth)
    {
        throw new NotImplementedException();
    }
}
