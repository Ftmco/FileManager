using File.Abstraction;
using File.DataBase.Context;
using File.Entity;
using File.Extension.Code;
using File.ViewModel;
using Identity.Client.Rules;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Implemention;

public class DirectoryAction : IDirectoryAction
{
    readonly IBaseCud<FDirectory, FileContext> _directoryCud;

    readonly IAccountRules _account;

    readonly IDirectoryViewModel _directoryViewModel;

    readonly IConfiguration _configuration;

    readonly IBaseQuery<FDirectory, FileContext> _fileQuery;

    public DirectoryAction(IBaseCud<FDirectory, FileContext> directoryCud, IAccountRules account,
        IDirectoryViewModel directoryViewModel, IConfiguration configuration, IBaseQuery<FDirectory, FileContext> fileQuery)
    {
        _directoryCud = directoryCud;
        _account = account;
        _directoryViewModel = directoryViewModel;
        _configuration = configuration;
        _fileQuery = fileQuery;
    }

    public async Task<UpsertDirectoryResponse> CreateAsync(UpsertDirectory create, Guid owneerId)
    {
        FDirectory directory = new()
        {
            CreateDate = DateTime.UtcNow,
            IsActive = true,
            LastUpdateDate = DateTime.UtcNow,
            Name = create.Name,
            Token = "Directory_" + 15.CreateToken(),
            ParentId = create.ParentId,
            OwnerId = owneerId
        };
        string path = "";
        if (directory.ParentId != null)
        {
            FDirectory? parent = await _fileQuery.GetAsync(directory.ParentId);
            if (parent != null)
            {
                directory.Path = parent.Path + $"/{directory.Name}";
                path = directory.Path;
            }
        }
        else
        {
            directory.Path = $"/{directory.Name}";
            path = directory.Path;
        }
        await CreateDirectoryAsync(path);
        return await _directoryCud.InsertAsync(directory)
            ? new UpsertDirectoryResponse(DirecttoryActionStatus.Success, await _directoryViewModel.CreateDirectoryViewModelAsync(directory))
            : new UpsertDirectoryResponse(DirecttoryActionStatus.Exception, null);
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public Task<UpsertDirectoryResponse> UpdateAsync(UpsertDirectory update, Guid ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task<UpsertDirectoryResponse> UpsertAsync(UpsertDirectory upsert, HttpContext httpContext)
    {
        string session = httpContext.Request.Headers["Auth-Token"];
        var user = await _account.GetUserCacheAsync(session);
        if (user != null)
            return upsert.Id == null ?
                    await CreateAsync(upsert, user.Id)
                        : await UpdateAsync(upsert, user.Id);

        return new UpsertDirectoryResponse(DirecttoryActionStatus.AccessDenied, null);
    }

    public Task<bool> CreateDirectoryAsync(string path)
    {
        try
        {
            string baseDir = _configuration["BaseDirectories:BaseDir"];
            string newDir = baseDir + path;
            if (!Directory.Exists(newDir))
                Directory.CreateDirectory(newDir);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}
