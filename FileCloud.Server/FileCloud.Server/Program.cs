using FileCloud.Server;
using FubarDev.FtpServer;
using FubarDev.FtpServer.FileSystem.DotNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Create Ftp Server 
builder.Services.AddFtpServer(builder =>
    builder.UseDotNetFileSystem()
        .EnableAnonymousAuthentication());
builder.Services.Configure<FtpServerOptions>(opt => opt.ServerAddress = "");
builder.Services.Configure<DotNetFileSystemOptions>(opt =>
        opt.RootPath = Path.Combine(Path.GetTempPath(), "Files"));
builder.Services.AddHostedService<HostedFtpService>();
//End Ftp Server


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
