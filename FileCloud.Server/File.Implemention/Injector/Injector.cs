using File.Implemention.Base;
using Identity.Client.Rules;
using Identity.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace File.Implemention.Injector;

public static class Injector
{
    public static async Task<IServiceCollection> AddFileServicesAsync(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FileContext>(options =>
        {
            string cnn = configuration.GetConnectionString("File_Db");
            FileContext.ConnectionString = cnn;
            options.UseNpgsql(cnn);
        });
        await services.AddBaseCudAsync();
        await services.AddBaseQueryAsync();
        await services.AddServicesAsync();
        await services.AddToolsAsync();
        return services;
    }

    public static Task<IServiceCollection> AddBaseQueryAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseQuery<FDirectory, FileContext>, BaseQuery<FDirectory, FileContext>>();
        services.AddScoped<IBaseQuery<DirectoryFile, FileContext>, BaseQuery<DirectoryFile, FileContext>>();
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddBaseCudAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseCud<FDirectory, FileContext>, BaseCud<FDirectory, FileContext>>();
        services.AddScoped<IBaseCud<DirectoryFile, FileContext>, BaseCud<DirectoryFile, FileContext>>();

        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddServicesAsync(this IServiceCollection services)
    {
        services.AddTransient<IDirectoryAction, DirectoryAction>();
        services.AddTransient<IDirectoryViewModel, DirectoryViewModel>();
        services.AddTransient<IDirectoryGet, DirectoryGet>();

        services.AddTransient<IFileAction, FileAction>();
        services.AddTransient<IFileViewModel, FileViewModel>();
        services.AddTransient<IFileGet, FileGet>();


        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddToolsAsync(this IServiceCollection services)
    {
        services.AddTransient<IAccountRules, AccountService>();
        return Task.FromResult(services);
    }
}