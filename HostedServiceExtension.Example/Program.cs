using HostedServiceExtension.Example;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
    })
    .Build();

host.Run();
