namespace HostedServiceExtension.CronosJobScheduler;

using System;

using Cronos;

using Microsoft.Extensions.Hosting;

#pragma warning disable CA1812
internal sealed class JobSchedulerService : BackgroundService
{
    private readonly List<JobOptions> jobOptions;

    public JobSchedulerService(IServiceProvider serviceProvider, Action<JobSchedulerOptions> config)
    {
        var options = new JobSchedulerOptions(serviceProvider);
        config(options);
        jobOptions = options.JobOptions;
    }

    public override void Dispose()
    {
        base.Dispose();

        foreach (var jobOption in jobOptions)
        {
            if (jobOption.Job is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new Task[jobOptions.Count];
        for (var i = 0; i < tasks.Length; i++)
        {
            tasks[i] = RunJobAsync(jobOptions[i], stoppingToken);
        }

        return Task.WhenAll(tasks);
    }

#pragma warning disable CA1031
    private static async Task RunJobAsync(JobOptions options, CancellationToken stoppingToken)
    {
        var expression = CronExpression.Parse(options.Expression);
        var timeZoneInfo = options.TimeZoneInfo;
        var job = options.Job;

        var next = expression.GetNextOccurrence(DateTimeOffset.Now, timeZoneInfo);
        while (!stoppingToken.IsCancellationRequested && next.HasValue)
        {
            try
            {
                var delay = next.Value - DateTimeOffset.Now;
                await Task.Delay(delay < TimeSpan.Zero ? TimeSpan.Zero : delay, stoppingToken).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);

                if (!stoppingToken.IsCancellationRequested)
                {
                    await job.ExecuteAsync(next.Value, stoppingToken).ConfigureAwait(false);
                }
            }
            catch
            {
                // Ignore
            }

            next = expression.GetNextOccurrence(next.Value, timeZoneInfo);
        }
    }
#pragma warning restore CA1031
}
#pragma warning restore CA1812
