namespace HostedServiceExtension.Example.Jobs;

using HostedServiceExtension.CronosJobScheduler;

public sealed class SampleJob : ISchedulerJob
{
    public ValueTask ExecuteAsync(DateTime time) => throw new NotImplementedException();
}
