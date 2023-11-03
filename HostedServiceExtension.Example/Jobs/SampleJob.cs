namespace HostedServiceExtension.Example.Jobs;

using HostedServiceExtension.CronosJobScheduler;

#pragma warning disable CA1848
public sealed class SampleJob : ISchedulerJob
{
    private readonly ILogger<SampleJob> log;

    public SampleJob(ILogger<SampleJob> log)
    {
        this.log = log;
    }

    public ValueTask ExecuteAsync(DateTime time)
    {
        log.LogInformation("Run at {Time:HH:mm:ss}.", time);
        return ValueTask.CompletedTask;
    }
}
#pragma warning restore CA1848
