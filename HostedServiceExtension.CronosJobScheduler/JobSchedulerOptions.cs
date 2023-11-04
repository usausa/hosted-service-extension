namespace HostedServiceExtension.CronosJobScheduler;

using Microsoft.Extensions.DependencyInjection;

public sealed class JobSchedulerOptions
{
    private readonly IServiceProvider serviceProvider;

    internal List<JobOptions> JobOptions { get; } = new();

    internal JobSchedulerOptions(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public void UseJob<T>(string expression, TimeZoneInfo? timeZoneInfo = null)
        where T : ISchedulerJob
    {
        JobOptions.Add(new JobOptions(expression, timeZoneInfo ?? TimeZoneInfo.Local, ActivatorUtilities.CreateInstance<T>(serviceProvider)));
    }
}
