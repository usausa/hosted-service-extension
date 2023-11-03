using HostedServiceExtension.Example.Service;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // TCP Server
        // TODO

        // Cron Job
        // TODO

        // Service
        services.AddSingleton<FeatureService>();
    })
    .Build();

host.Run();
