namespace HostedServiceExtension.KestrelTcpServer;

using System.Net;

using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;

public sealed class KestrelTcpServiceOptions
{
    private readonly KestrelServerOptions serverOptions;

    public SocketTransportOptions TransportOptions { get; }

    public bool GracefulShutdown { get; set; }

    public KestrelTcpServiceOptions(KestrelServerOptions serverOptions, SocketTransportOptions transportOptions)
    {
        this.serverOptions = serverOptions;
        TransportOptions = transportOptions;
    }

    public void Listen<T>(IPAddress address, int port)
        where T : ConnectionHandler =>
        serverOptions.Listen(address, port, config =>
        {
            config.Protocols = HttpProtocols.None;
            config.UseConnectionHandler<T>();
        });

    public void Listen<T>(IPEndPoint endPoint)
        where T : ConnectionHandler =>
        serverOptions.Listen(endPoint, config =>
        {
            config.Protocols = HttpProtocols.None;
            config.UseConnectionHandler<T>();
        });

    public void ListenLocalhost<T>(int port)
        where T : ConnectionHandler =>
        serverOptions.ListenLocalhost(port, config =>
        {
            config.Protocols = HttpProtocols.None;
            config.UseConnectionHandler<T>();
        });

    public void ListenAnyIP<T>(int port)
        where T : ConnectionHandler =>
        serverOptions.ListenAnyIP(port, config =>
        {
            config.Protocols = HttpProtocols.None;
            config.UseConnectionHandler<T>();
        });
}
