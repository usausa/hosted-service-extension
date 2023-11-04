namespace HostedServiceExtension.KestrelTcpServer;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTcpServer(this IServiceCollection services, Action<TcpServerOptions> options)
    {
        services.AddSingleton(options);
        services.AddHostedService<TcpServerService>();
        return services;
    }
}
