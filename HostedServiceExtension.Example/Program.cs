using HostedServiceExtension.CronosJobScheduler;
using HostedServiceExtension.Example.Handlers;
using HostedServiceExtension.Example.Handlers.Commands;
using HostedServiceExtension.Example.Jobs;
using HostedServiceExtension.Example.Service;
using HostedServiceExtension.KestrelTcpServer;

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
