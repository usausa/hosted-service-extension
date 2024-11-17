# HostedService Extension for .NET

[![NuGet](https://img.shields.io/nuget/v/HostedServiceExtension.KestrelTcpServer.svg)](https://www.nuget.org/packages/HostedServiceExtension.KestrelTcpServer)
[![NuGet](https://img.shields.io/nuget/v/HostedServiceExtension.CronosJobScheduler.svg)](https://www.nuget.org/packages/HostedServiceExtension.CronosJobScheduler)

```csharp
var builder = Host.CreateApplicationBuilder(args);

// TCP Server
builder.Services.AddTcpServer(options =>
{
    options.ListenAnyIP<SampleHandler>(18888);
});
builder.Services.AddSingleton<ICommand, ExitCommand>();
builder.Services.AddSingleton<ICommand, GetCommand>();
builder.Services.AddSingleton<ICommand, SetCommand>();

// Cron Job
builder.Services.AddJobScheduler(options =>
{
    options.UseJob<SampleJob>("*/1 * * * *");
});

// Service
builder.Services.AddSingleton<FeatureService>();

builder.Build().Run();
```
