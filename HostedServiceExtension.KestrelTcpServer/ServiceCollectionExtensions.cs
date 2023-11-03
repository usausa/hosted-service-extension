namespace HostedServiceExtension.KestrelTcpServer;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKestrelTcpService(this IServiceCollection services, Action<KestrelTcpServiceOptions> options)
    {
        services.AddSingleton(new KestrelTcpServiceConfig { Configure = options });
        services.AddHostedService<KestrelTcpService>();
        return services;
    }
}
