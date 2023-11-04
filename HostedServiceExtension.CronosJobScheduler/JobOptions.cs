namespace HostedServiceExtension.CronosJobScheduler;

internal sealed class JobOptions
{
    public string Expression { get; }

    public TimeZoneInfo TimeZoneInfo { get; }

    public ISchedulerJob Job { get; }

    public JobOptions(string expression, TimeZoneInfo timeZoneInfo, ISchedulerJob job)
    {
        Expression = expression;
        TimeZoneInfo = timeZoneInfo;
        Job = job;
    }
}
