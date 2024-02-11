using HostedServiceExtension.CronosJobScheduler;
using HostedServiceExtension.Example.Handlers;
using HostedServiceExtension.Example.Handlers.Commands;
using HostedServiceExtension.Example.Jobs;
using HostedServiceExtension.Example.Service;
using HostedServiceExtension.KestrelTcpServer;

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
