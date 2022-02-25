using FubarDev.FtpServer;

namespace FileCloud.Server;

public class HostedFtpService : IHostedService
{
    readonly IFtpServerHost _ftpServerHost;

    public HostedFtpService(IFtpServerHost ftpServerHost)
    {
        _ftpServerHost = ftpServerHost;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return _ftpServerHost.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _ftpServerHost.StopAsync(cancellationToken);
    }
}
