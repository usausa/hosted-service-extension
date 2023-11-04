# HostedService Extension for .NET

[![NuGet Badge](https://buildstats.info/nuget/HostedServiceExtension.KestrelTcpServer)](https://www.nuget.org/packages/HostedServiceExtension.KestrelTcpServer/)
[![NuGet Badge](https://buildstats.info/nuget/HostedServiceExtension.CronosJobScheduler)](https://www.nuget.org/packages/HostedServiceExtension.CronosJobScheduler/)

```csharp
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // TCP Server
        services.AddTcpServer(options =>
        {
            options.ListenAnyIP<SampleHandler>(18888);
        });
        services.AddSingleton<ICommand, ExitCommand>();
        services.AddSingleton<ICommand, GetCommand>();
        services.AddSingleton<ICommand, SetCommand>();

        // Cron Job
        services.AddJobScheduler(options =>
        {
            options.UseJob<SampleJob>("*/1 * * * *");
        });

        // Service
        services.AddSingleton<FeatureService>();
    })
    .Build();

host.Run();
```
