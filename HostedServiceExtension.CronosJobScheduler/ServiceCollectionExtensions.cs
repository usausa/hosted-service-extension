namespace HostedServiceExtension.CronosJobScheduler;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJobScheduler(this IServiceCollection services, Action<JobSchedulerOptions> options)
    {
        services.AddSingleton(options);
        services.AddHostedService<JobSchedulerService>();
        return services;
    }
}
