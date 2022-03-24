using File.DataBase.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using File.Implemention.Base;
using File.Entity.Application;

namespace File.Implemention.Injector;

public static class Injector
{
    public static async Task<IServiceCollection> AddFileServicesAsync(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FileContext>(options =>
        {
            string cnn = configuration.GetConnectionString("File_Db");
            FileContext.ConnectionString = cnn;
            options.UseSqlServer(cnn);
        });
        await services.AddBaseCudAsync();
        await services.AddBaseQueryAsync();
        await services.AddServicesAsync();
        await services.AddToolsAsync();
        return services;
    }

    public static Task<IServiceCollection> AddBaseQueryAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseQuery<Application, FileContext>, BaseQuery<Application, FileContext>>();
        services.AddScoped<IBaseQuery<Entity.File.Directory, FileContext>, BaseQuery<Entity.File.Directory, FileContext>>();
        services.AddScoped<IBaseQuery<Entity.File.File, FileContext>, BaseQuery<Entity.File.File, FileContext>>();
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddBaseCudAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseCud<Application, FileContext>, BaseCud<Application, FileContext>>();
        services.AddScoped<IBaseCud<Entity.File.Directory, FileContext>, BaseCud<Entity.File.Directory, FileContext>>();
        services.AddScoped<IBaseCud<Entity.File.File, FileContext>, BaseCud<Entity.File.File, FileContext>>();
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddServicesAsync(this IServiceCollection services)
    {
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddToolsAsync(this IServiceCollection services) { return Task.FromResult(services); }
}