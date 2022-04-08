using File.DataBase.Context;
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
      
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddBaseCudAsync(this IServiceCollection services)
    {
       
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddServicesAsync(this IServiceCollection services)
    {
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddToolsAsync(this IServiceCollection services) { return Task.FromResult(services); }
}