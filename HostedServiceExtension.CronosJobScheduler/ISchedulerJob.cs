namespace HostedServiceExtension.CronosJobScheduler;

public interface ISchedulerJob
{
    ValueTask ExecuteAsync(DateTimeOffset time, CancellationToken cancellationToken);
}
