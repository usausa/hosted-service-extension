using HostedServiceExtension.Example.Handlers;
using HostedServiceExtension.Example.Handlers.Commands;
using HostedServiceExtension.Example.Service;
using HostedServiceExtension.KestrelTcpServer;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // TCP Server
        services.AddKestrelTcpService(options =>
        {
            options.ListenAnyIP<SampleHandler>(8888);
        });
        services.AddSingleton<ICommand, ExitCommand>();
        services.AddSingleton<ICommand, GetCommand>();
        services.AddSingleton<ICommand, SetCommand>();

        // Cron Job
        // TODO

        // Service
        services.AddSingleton<FeatureService>();
    })
    .Build();

host.Run();
