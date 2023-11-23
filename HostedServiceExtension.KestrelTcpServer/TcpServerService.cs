namespace HostedServiceExtension.KestrelTcpServer;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

#pragma warning disable CA1812
internal sealed class TcpServerService : IHostedService, IDisposable
{
    private readonly KestrelServer kestrelServer;

    private readonly bool gracefulShutdown;

    public TcpServerService(IServiceProvider serviceProvider, Action<TcpServerOptions> config)
    {
        var serverOptions = new KestrelServerOptions
        {
            ApplicationServices = serviceProvider
        };
        var transportOptions = new SocketTransportOptions();

        var options = new TcpServerOptions(serverOptions, transportOptions);
        config(options);

        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var transportFactory = new SocketTransportFactory(new OptionsWrapper<SocketTransportOptions>(transportOptions), loggerFactory);
        kestrelServer = new KestrelServer(new OptionsWrapper<KestrelServerOptions>(serverOptions), transportFactory, loggerFactory);

        gracefulShutdown = options.GracefulShutdown;
    }

    public void Dispose() => kestrelServer.Dispose();

    public Task StartAsync(CancellationToken cancellationToken) =>
        kestrelServer.StartAsync((IHttpApplication<object>)null!, cancellationToken);

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (gracefulShutdown)
        {
            await kestrelServer.StopAsync(cancellationToken).ConfigureAwait(false);
        }
        else
        {
            using var cts = new CancellationTokenSource();
            await cts.CancelAsync().ConfigureAwait(false);
            await kestrelServer.StopAsync(cts.Token).ConfigureAwait(false);
        }
    }
}
#pragma warning restore CA1812
