namespace HostedServiceExtension.CronosJobScheduler;

public interface ISchedulerJob
{
    ValueTask ExecuteAsync(DateTime time);
}
