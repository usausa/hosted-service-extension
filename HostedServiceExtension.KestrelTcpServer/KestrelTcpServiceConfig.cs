namespace HostedServiceExtension.KestrelTcpServer;

internal sealed class KestrelTcpServiceConfig
{
    public Action<KestrelTcpServiceOptions> Configure { get; set; } = default!;
}
